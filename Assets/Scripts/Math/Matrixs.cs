using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrixs : MonoBehaviour
{
    private void Start()
    {
        Concatenatiny();
    }

    void MatrixTricks()
    {
        Matrix4x4 matrix = new Matrix4x4();
        var m00 = matrix[0, 0];
        matrix[0, 1] = 2f;
    }

    void Move() //moves (translates) vector by 5 units on the x-axis
    {
        var translationMatrix = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(5, 0, 0, 1)
        );

        var input = new Vector3(0, 1, 2);
        var result = translationMatrix.MultiplyPoint(input);
        print(result);

        var otherTranslationMatrix = Matrix4x4.Translate(new Vector3(5, 0, 0));
        result = otherTranslationMatrix.MultiplyPoint(input);
        print(result);
    }

    void Rotate()
    {
        var rotate90DegreesAroundX = Quaternion.Euler(90, 0, 0);
        var rotationMatrix = Matrix4x4.Rotate(rotate90DegreesAroundX);
        var input = new Vector3(0, 0, 1);
        var result = rotationMatrix.MultiplyVector(input);
        print(result);
    }

    void Scale()
    {
        var scale2x2x2 = Matrix4x4.Scale(new Vector3(2f, 2f, 2f));
        var input = new Vector3(1f, 2f, 3f);
        var result = scale2x2x2.MultiplyPoint3x4(input);
        print(result);
    }

    void Concatenatiny()
    {
        var translation = Matrix4x4.Translate(new Vector3(5, 0, 0));
        var rotation = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));
        var scale = Matrix4x4.Scale(new Vector3(1, 5, 1));

        var combined = translation * rotation * scale;

        var input = Vector3.one;
        var result = combined.MultiplyPoint(input);
        print(result);

        var transformMatrix = Matrix4x4.TRS(
            new Vector3(5, 0, 0),
            Quaternion.Euler(90, 0, 0),
            new Vector3(1, 5, 1)
         );

        result = transformMatrix.MultiplyPoint(input);
        print(result);
    }

    void LocalToWorld()
    {
        var localToWorld = this.transform.localToWorldMatrix;
    }
}
