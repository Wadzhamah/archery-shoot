using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Puzzle : MonoBehaviour
{
    public int ID; // номер должен соответствовать данной "костяшке"

    public void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // текущая и пустая клетка, меняются местами
    void ReplaceBlocks(int x, int y, int XX, int YY)
    {
        Transform currentTransform = GameController.grid[x, y].transform;
        Vector3 targetPosition = GameController.position[XX, YY];
        
        // Используйте DOTween для анимации перемещения
        currentTransform.DOMove(targetPosition, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            GameController.grid[x, y].transform.position = targetPosition;
            GameController.grid[XX, YY] = GameController.grid[x, y];
            GameController.grid[x, y] = null;
            GameController.Instance.GameFinish();
        });
    }

    void OnMouseDown()
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (GameController.grid[x, y])
                {
                    if (GameController.grid[x, y].GetComponent<Puzzle>().ID == ID)
                    {
                        if (x > 0 && GameController.grid[x - 1, y] == null)
                        {
                            ReplaceBlocks(x, y, x - 1, y);
                            return;
                        }
                        else if (x < 3 && GameController.grid[x + 1, y] == null)
                        {
                            ReplaceBlocks(x, y, x + 1, y);
                            return;
                        }
                    }
                }
                if (GameController.grid[x, y])
                {
                    if (GameController.grid[x, y].GetComponent<Puzzle>().ID == ID)
                    {
                        if (y > 0 && GameController.grid[x, y - 1] == null)
                        {
                            ReplaceBlocks(x, y, x, y - 1);
                            return;
                        }
                        else if (y < 3 && GameController.grid[x, y + 1] == null)
                        {
                            ReplaceBlocks(x, y, x, y + 1);
                            return;
                        }
                    }
                }
            }
        }
    }
}
