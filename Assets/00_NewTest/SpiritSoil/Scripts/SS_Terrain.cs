using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_Terrain : MonoBehaviour
{
    public SpiritSoil spiritSoil;

    public bool forward;
    public bool right;
    public bool back;
    public bool left;
    public bool forwardRight;
    public bool rightBack;
    public bool backLeft;
    public bool leftForward;



    private void Update()
    {
        UpdateModel();
    }

    public void UpdateModel()
    {
        if (!spiritSoil.selected)
        {
            string ID = spiritSoil.ID;
            Vector3Int coord = spiritSoil.coord;
            forward = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.forward, ID);
            right = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.right, ID);
            back = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.back, ID);
            left = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.left, ID);
            forwardRight = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.forward + Vector3Int.right, ID);
            rightBack = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.right + Vector3Int.back, ID);
            backLeft = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.back + Vector3Int.left, ID);
            leftForward = SpiritSoilManager.Instance.SpiritSoilCheck(coord + Vector3Int.left + Vector3Int.forward, ID);
            int index = 0;
            if (forward && !right && !back && !left)
            { index = 1; }
            else if (!forward && right && !back && !left)
            { index = 2; }
            else if (!forward && !right && back && !left)
            { index = 3; }
            else if (!forward && !right && !back && left)
            { index = 4; }
            else if (forward && !right && back && !left)
            { index = 5; }
            else if (!forward && right && !back && left)
            { index = 6; }
            else if (forward && right && !back && !left)
            { index = forwardRight ? 16 : 7; }
            else if (!forward && right && back && !left)
            { index = rightBack ? 17 : 8; }
            else if (!forward && !right && back && left)
            { index = backLeft ? 18 : 9; }
            else if (forward && !right && !back && left)
            { index = leftForward ? 19 : 10; }
            else if (forward && right && back && !left)
            {
                if (forwardRight && rightBack) { index = 28; }
                else if (forwardRight && !rightBack) { index = 20; }
                else if (!forwardRight && rightBack) { index = 24; }
                else { index = 11; }
            }
            else if (!forward && right && back && left)
            {
                if (rightBack && backLeft) { index = 29; }
                else if (rightBack && !backLeft) { index = 21; }
                else if (!rightBack && backLeft) { index = 25; }
                else { index = 12; }
            }
            else if (forward && !right && back && left)
            {
                if (backLeft && leftForward) { index = 30; }
                else if (backLeft && !leftForward) { index = 22; }
                else if (!backLeft && leftForward) { index = 26; }
                else { index = 13; }
            }
            else if (forward && right && !back && left)
            {
                if (leftForward && forwardRight) { index = 31; }
                else if (leftForward && !forwardRight) { index = 23; }
                else if (!leftForward && forwardRight) { index = 27; }
                else { index = 14; }
            }
            else if (forward && right && back && left)
            {
                if (forwardRight && rightBack && backLeft && leftForward)
                { index = 46; }
                else if (forwardRight && !rightBack && !backLeft && !leftForward)
                { index = 32; }
                else if (!forwardRight && rightBack && !backLeft && !leftForward)
                { index = 33; }
                else if (!forwardRight && !rightBack && backLeft && !leftForward)
                { index = 34; }
                else if (!forwardRight && !rightBack && !backLeft && leftForward)
                { index = 35; }
                else if (forwardRight && rightBack && !backLeft && !leftForward)
                { index = 36; }
                else if (!forwardRight && rightBack && backLeft && !leftForward)
                { index = 37; }
                else if (!forwardRight && !rightBack && backLeft && leftForward)
                { index = 38; }
                else if (forwardRight && !rightBack && !backLeft && leftForward)
                { index = 39; }
                else if (forwardRight && !rightBack && backLeft && !leftForward)
                { index = 40; }
                else if (!forwardRight && rightBack && !backLeft && leftForward)
                { index = 41; }
                else if (forwardRight && rightBack && backLeft && !leftForward)
                { index = 42; }
                else if (!forwardRight && rightBack && backLeft && leftForward)
                { index = 43; }
                else if (forwardRight && !rightBack && backLeft && leftForward)
                { index = 44; }
                else if (forwardRight && rightBack && !backLeft && leftForward)
                { index = 45; }
                else { index = 15; }
            }
            spiritSoil.UpdateModel(spiritSoil.ID + '-' + index);
        }
        else { spiritSoil.UpdateModel(spiritSoil.ID + '-' + 0); }
    }
}
