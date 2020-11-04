﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCube : MonoBehaviour
{
}

public class Scenario : Global
{
  public GameObject tutorialCubePrefab;
  public TutorialCube[] tutorialCubes = new TutorialCube[9];

  public new void Awake()
  {
    scoreWhite = GameObject.Find("scoreWhite").GetComponent<Text>();
    scoreBlack = GameObject.Find("scoreBlack").GetComponent<Text>();
    this.GeneratePieces();
    for (var i = 0; i < tutorialCubes.Length; i++)
    {
      tutorialCubes[i] = GenerateCube(tutorialCubePrefab);
    }
    this.InitialiseScenario();
  }

  /**
  * Single Piece Generator.
  */
  private TutorialCube GenerateCube(GameObject cubePrefab)
  {
    GameObject go = Instantiate(cubePrefab) as GameObject;
    go.AddComponent<TutorialCube>();
    TutorialCube tutorialCube = go.GetComponent<TutorialCube>();
    return tutorialCube;
  }

  private void PlaceCube(Piece piece, Vector2 piecePosition)
  {
    if (selectedPiece)
    {
      int count = 0;
      for (var i = -2; i < 4; i += 2)
      {
        for (var j = -2; j < 4; j += 2)
        {
          Vector2 boardCoordinate = new Vector2((i + piecePosition.x), (j + piecePosition.y));
          Piece otherPiece = GetPiece(boardCoordinate);
          bool isPossible = false;
          // Move
          if (selectedPiece != null && (otherPiece == null) && GameLogic.IsMovePossible(piece.isEcce, true,
            ToBoardCoordinates(piecePosition), ToBoardCoordinates(boardCoordinate)))
          {
            isPossible = true;
          }
          // Eat
          if (otherPiece != null && GameLogic.IsMovePossible(
            true, false, ToBoardCoordinates(piecePosition),
            ToBoardCoordinates(boardCoordinate)))
          {
            isPossible = true;
          }
          // Ecce
          if(otherPiece &&
            !otherPiece.name.Contains(selectedPiece.name.Substring(0, 5)) && piece.isEcce)
          {
            isPossible = true;
          }
          if (isPossible)
          {
            tutorialCubes[count].transform.position =
              (Vector3.right * ToBoardCoordinates(boardCoordinate).x) +
              (Vector3.forward * ToBoardCoordinates(boardCoordinate).y) +
              (Vector3.up * -0.5f);
            count++;
          }
        }
      }
    } else
    {
      RemoveCube();
    }
  }

  private void RemoveCube()
  {
    for (int i = 0; i < 9; i++)
    {
      tutorialCubes[i].transform.position =
            (Vector3.right * -10) +
            (Vector3.forward * -10) +
            (Vector3.up * -0.5f);
    }
  }

  void InitialiseScenario ()
  {
    TryPlaceNewPiece(0);
    TryPlaceNewPiece(1);
    TrySelectPiece(new Vector2(2, 2), 0);
    TryMovePiece(selectedPiece,
      new Vector2(5 * caseLength, 1 * caseLength),
      new Vector2(1 * caseLength, 1 * caseLength));
  }

  public override void TrySelectPiece(Vector2 mouseOver, int player)
  {
    base.TrySelectPiece(mouseOver, player);
    PlaceCube(selectedPiece, mouseOver);
  }

  public override void TryMovePiece(Piece p, Vector2 mouseOver, Vector2 startDrag)
  {
    base.TryMovePiece(p, mouseOver, startDrag);
    RemoveCube();
  }
}