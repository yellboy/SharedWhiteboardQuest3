using System.Collections.Concurrent;
using System.Collections.Generic;
using Assets.Model;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class WhiteboardDrawing : MonoBehaviour
    {
        [SerializeField] private PokeInteractable _pokeInteractable;

        [SerializeField] private RawImage _image;

        [SerializeField] private RectTransform _canvasRectTransform;

        private Texture2D _texture;

        private Vector2 _lastTexturePosition;
        private PointerEventType _lastPointerEventType;

        // Start is called before the first frame update
        private void Start()
        {
            _pokeInteractable.WhenPointerEventRaised += PokeInteractable_WhenPointerEventRaised;
            // Initialize the texture (assuming a 256x256 texture for this example)
            _texture = new Texture2D((int)_canvasRectTransform.sizeDelta.x, (int)_canvasRectTransform.sizeDelta.y, TextureFormat.RGBA32, false);

            // Optionally, fill the texture with a default color
            Color fillColor = new Color(0.85f, 0.85f, 0.85f, 1);
            Color[] fillPixels = _texture.GetPixels();
            for (int i = 0; i < fillPixels.Length; i++)
            {
                fillPixels[i] = fillColor;
            }
            _texture.SetPixels(fillPixels);
            _texture.Apply();

            // Assign the texture to the RawImage component
            _image.texture = _texture;
        }

        private void PokeInteractable_WhenPointerEventRaised(PointerEvent obj)
        {
            print($"Poke event: {obj.Type}");
            if (obj.Type == PointerEventType.Move)
            {
                var position = WorldToCanvasPosition(obj.Pose.position, _canvasRectTransform);

                var texturePosition = CanvasPositionToTexturePosition(position, _texture);

                print($"Poke moved, world: {obj.Pose.position}, canvas: {position}, texture: {texturePosition}");

                if (_lastPointerEventType == PointerEventType.Move)
                {
                    DrawLine(_lastTexturePosition, texturePosition);
                }
                else
                {
                    ColorPixel(texturePosition, 5);
                }

                _texture.Apply();

                _lastTexturePosition = texturePosition;
            }

            _lastPointerEventType = obj.Type;
        }

        private void DrawLine(Vector2 position1, Vector2 position2)
        {
            var x1 = (int)position1.x;
            var y1 = (int)position1.y;
            var x2 = (int)position2.x;
            var y2 = (int)position2.y;
            var dx = Mathf.Abs(x2 - x1);
            var dy = Mathf.Abs(y2 - y1);
            var sx = x1 < x2 ? 1 : -1;
            var sy = y1 < y2 ? 1 : -1;
            var err = dx - dy;

            while (true)
            {
                ColorPixel(new Vector2(x1, y1), 5);

                if (x1 == x2 && y1 == y2)
                {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        private void ColorPixel(Vector2 texturePosition, int blockSize)
        {
            var colorArray = new Color[blockSize * blockSize];
            for (var i = 0; i < colorArray.Length; i++)
            {
                colorArray[i] = Color.black;
            }

            _texture.SetPixels((int)texturePosition.x, (int)texturePosition.y, blockSize, blockSize, colorArray);
        }

        private Vector2 CanvasPositionToTexturePosition(Vector2 position, Texture2D texture)
        {
            var x = position.x + texture.width / 2;
            var y = position.y + texture.height / 2;

            return new Vector2(x, y);
        }

        private static Vector2 WorldToCanvasPosition(Vector3 worldPosition, RectTransform canvasRectTransform)
        {
            // Get the canvas size (assuming it's a RectTransform)
            Vector3 viewportPosition = Camera.main.WorldToScreenPoint(worldPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform,
                viewportPosition, Camera.main, out var localPoint);

            return localPoint;
        }

        public void UpdateWhiteboard(Whiteboard whiteboard)
        {
            foreach (var pixel in whiteboard.ColoredPoints)
            {
                ColorPixel(new Vector2(pixel.X, pixel.Y), 1);
            }

            _texture.Apply(true);
        }
    }
}
