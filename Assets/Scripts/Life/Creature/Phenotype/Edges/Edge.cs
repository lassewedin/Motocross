﻿using UnityEngine;
using System;

public class Edge : MonoBehaviour {
    public GameObject mainArrow;
    public GameObject normalArrow;
    public GameObject velocityArrow;
    public GameObject forceArrow;
    
    public float mEnergyTransfer = 0.5f; //directional ?
    public bool mIsBranch = false; //if branch then A is mother

    private bool mIsWing = false;
    private SpringJoint2D mSpring;
    private EdgeAttachment attachmentParent;
    private EdgeAttachment attachmentChild;

    private Cell frontCell; //used with wings
    private Cell backCell; //used with wings
     
    private Vector3 normal; //used with wings
    private Vector3 velocity; //used with wings
    private Vector3 force; //used with wings


    public void Setup(Cell parentCell, Cell childCell, int directionChildToParentCell, bool isBranch) {
        attachmentParent = new EdgeAttachment(parentCell, (directionChildToParentCell + 3 ) % 6);
        attachmentChild = new EdgeAttachment(childCell, directionChildToParentCell);
        this.mIsBranch = isBranch;
   }

    public SpringJoint2D spring {
        get { return mSpring; }
        private set { } 
    }

    public bool isWing {
        get { return mIsWing; }
        private set { }
    }

    public Cell GetOtherCell(Cell cell) {
        if (cell == attachmentParent.cell) {
            return attachmentChild.cell;
        }
        else if (cell == attachmentChild.cell) {
            return attachmentParent.cell;
        }
        return null;
    }

    public void UpdateSpring() {

    }

    public void UpdateEnergyTransfer() {

    }

    public void MakeWing(Cell frontCell) {
        if (frontCell == attachmentParent.cell) {
            this.frontCell = frontCell;
            this.backCell = attachmentChild.cell;
        }
        else if (frontCell == attachmentChild.cell) {
            this.frontCell = frontCell;
            this.backCell = attachmentParent.cell;
        }
        else {
            throw new Exception("Trying to make a wing, with frontCell wich is not present in edge");
        }
        this.mIsWing = true;
    }

    public void Update() {
        if (frontCell != null && backCell != null) {
            //draw main
            mainArrow.GetComponent<LineRenderer>().SetPosition(1, frontCell.transform.position);
            mainArrow.GetComponent<LineRenderer>().SetPosition(0, backCell.transform.position);

            //draw normal
            Vector3 wingSegmentHalf = (frontCell.transform.position - backCell.transform.position) * 0.5f;
            Vector3 midSegment = backCell.transform.position + wingSegmentHalf;
            Vector3 normalPoint = midSegment + normal;
            normalArrow.GetComponent<LineRenderer>().SetPosition(1, normalPoint);
            normalArrow.GetComponent<LineRenderer>().SetPosition(0, midSegment);

            //draw velocity
            velocityArrow.GetComponent<LineRenderer>().SetPosition(1, midSegment + velocity);
            velocityArrow.GetComponent<LineRenderer>().SetPosition(0, midSegment);

            //draw force
            forceArrow.GetComponent<LineRenderer>().SetPosition(1, midSegment + force * 100f);
            forceArrow.GetComponent<LineRenderer>().SetPosition(0, midSegment);
        }
    }

    public static float maxForce = 0f;

    //Use 2 cells to find normal, allways on wings RIGHT hand
    public void UpdateNormal() {
        Vector3 wingSegment = frontCell.transform.position - backCell.transform.position;
        normal = Vector3.Cross(wingSegment.normalized, new Vector3(0f, 0f, 1f));
    }



    //use 2 cells to find center velocity
    public void UpdateVelocity() {
        velocity = (frontCell.GetComponent<Rigidbody2D>().velocity + backCell.GetComponent<Rigidbody2D>().velocity) / 2f;
    }

    // use normal and velocity to calculate force
    public void UpdateForce(Vector3 creatureVelocity, Creature creature) {

        float velocityInNormalDirection = Math.Max(0f, Vector3.Dot(normal, velocity - creatureVelocity * (1f - creature.wingDrag)));
        force = -normal * Math.Min(creature.wingMax, (creature.f1 * velocityInNormalDirection + creature.wingF2 * Mathf.Pow(velocityInNormalDirection, creature.wingPow)));
    }

    //Apply current force as an impulse on cells
    public void ApplyForce() {
        frontCell.GetComponent<Rigidbody2D>().AddForce(force * 0.5f, ForceMode2D.Impulse);
        backCell.GetComponent<Rigidbody2D>().AddForce(force * 0.5f, ForceMode2D.Impulse);

        //frontCell.GetComponent<Rigidbody2D>().AddForce(force *5f, ForceMode2D.Force);
        //backCell.GetComponent<Rigidbody2D>().AddForce(force * 5f, ForceMode2D.Force);
    }
}

