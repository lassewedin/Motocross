using UnityEngine;
using System.Collections.Generic;

public class MuscleCell : Cell {

    public MuscleCell() : base() {
        springFrequenzy = 20f;
    }

    float modularTime = 0f;

    float lastTime = 0;
    public override void UpdateRadius(float time) {
        float muscleSpeed = creature.muscleSpeed;
        float radiusDiff = creature.muscleRadiusDiff;
        float curveOffset = creature.muscleContractRetract;

        modularTime += Time.fixedDeltaTime * muscleSpeed;

        float deltaTime = time - lastTime;
        lastTime = time;

        float radiusGoal = 0.5f - 0.5f * radiusDiff + 0.5f * radiusDiff * Mathf.Sign(curveOffset + Mathf.Cos(timeOffset + modularTime / (2f * Mathf.PI)));

        float goingSmallSpeed = 0.1f; //units per second
        float goingBigSpeed = 0.02f;

        if (radiusGoal > radius) {
            radius = radius + goingBigSpeed * deltaTime;
            if (radius > radiusGoal)
                radius = radiusGoal;
        }
        else {
            radius = radius - goingSmallSpeed * deltaTime;
            if (radius < radiusGoal)
                radius = radiusGoal;
        }

        gameObject.transform.localScale = new Vector3(radius * 2, radius * 2, 1f);

        /*float specialMuscleSpeed = 6f;
        if (type == CellType.Leaf) {
            float radiusDiff = 0.2f;
            float radiusGoal = 0.5f - 0.5f * radiusDiff + 0.5f * radiusDiff * Mathf.Sign(Mathf.Cos(-Mathf.PI * 0.5f + Mathf.PI + time * specialMuscleSpeed / (2f * Mathf.PI)));

            float goingSmallSpeed = 0.1f; //units per second
            float goingBigSpeed = 0.1f;

            if (radiusGoal > radius) {
                radius = radius + goingBigSpeed * Time.fixedDeltaTime;
                if (radius > radiusGoal)
                    radius = radiusGoal;
            }
            else {
                radius = radius - goingSmallSpeed * Time.fixedDeltaTime;
                if (radius < radiusGoal)
                    radius = radiusGoal;
            }

            gameObject.transform.localScale = new Vector3(radius * 2, radius * 2, 1f);
        }
        if (type == CellType.Mouth) {
            float radiusDiff = 0.2f;
            float radiusGoal = 0.5f - 0.5f * radiusDiff + 0.5f * radiusDiff * Mathf.Sign(Mathf.Cos(Mathf.PI * 0.5f + Mathf.PI + time * specialMuscleSpeed / (2f * Mathf.PI)));

            float goingSmallSpeed = 0.1f; //units per second
            float goingBigSpeed = 0.1f;

            if (radiusGoal > radius) {
                radius = radius + goingBigSpeed * Time.fixedDeltaTime;
                if (radius > radiusGoal)
                    radius = radiusGoal;
            }
            else {
                radius = radius - goingSmallSpeed * Time.fixedDeltaTime;
                if (radius < radiusGoal)
                    radius = radiusGoal;
            } 

            gameObject.transform.localScale = new Vector3(radius * 2, radius * 2, 1f);
        }*/
    }

    public override void UpdateSpringLengths() {

        if (HasNeighbour(CardinalDirection.northEast)) {
            northEastNeighbour.cell.GetSpring(this).distance = this.radius + northEastNeighbour.cell.radius;
        }

        if (HasNeighbour(CardinalDirection.north)) {
            northSpring.distance = this.radius + northNeighbour.cell.radius;
        }

        if (HasNeighbour(CardinalDirection.northWest)) {
            northWestNeighbour.cell.GetSpring(this).distance = this.radius + northWestNeighbour.cell.radius;
        }

        if (HasNeighbour(CardinalDirection.southWest)) {
            southWestSpring.distance = this.radius + southWestNeighbour.cell.radius;
        }

        if (HasNeighbour(CardinalDirection.south)) {
            southNeighbour.cell.GetSpring(this).distance = this.radius + southNeighbour.cell.radius;
        }

        if (HasNeighbour(CardinalDirection.southEast)) {
            southEastSpring.distance = this.radius + southEastNeighbour.cell.radius;
        }
    }

    public override void UpdateSpringFrequenzy() {

        if (HasNeighbour(CardinalDirection.northEast)) {
            northEastNeighbour.cell.GetSpring(this).frequency = this.springFrequenzy;
        }

        if (HasNeighbour(CardinalDirection.north)) {
            northSpring.frequency = this.springFrequenzy;
        }

        if (HasNeighbour(CardinalDirection.northWest)) {
            northWestNeighbour.cell.GetSpring(this).frequency = this.springFrequenzy;
        }

        if (HasNeighbour(CardinalDirection.southWest)) {
            southWestSpring.frequency = this.springFrequenzy;
        }

        if (HasNeighbour(CardinalDirection.south)) {
            southNeighbour.cell.GetSpring(this).frequency = this.springFrequenzy;
        }

        if (HasNeighbour(CardinalDirection.southEast)) {
            southEastSpring.frequency = this.springFrequenzy;
        }
    }

}

