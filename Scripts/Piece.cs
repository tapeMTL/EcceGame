﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Each Piece represents an instanciated game object, either _white or _black
 */ 
public class Piece: MonoBehaviour {

    /**** Dependency ****/
    public NPalBoard Board { get; private set; }
    public bool isEcce = false;
    public Material[] myMaterials = new Material[2];

  /**** View ****/
  // Space between spaces
  protected int pieceMargin = 2;
}