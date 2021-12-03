using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewScript : MonoBehaviour
{
    // private void Start() {
    //     string ninoTest = "AR999311";
    //     Debug.Log(ValidateNino(ninoTest));
    // }
    //
    // // Define Func Conditional(s):
    // private readonly Func<string, bool> _mustHaveLengthOf9 = x => x.Length == 9;
    //
    // // Define Function to Check NINO:
    // public bool ValidateNino(string nino) =>
    //     new[] {
    //         _mustHaveLengthOf9
    //     }.All(x => x(nino.Replace(" ", "")));
    //
    //
    // // Define Validate Function:
    // public static bool Validate<TInput>(this TInput @this, params Func<TInput, bool>[] predicates) =>
    //     predicates.All(x => x(@this));
    //
    // // Define Function to Check NINO:
    // public bool ValidateNino2(string nino) =>
    //     nino.Replace(" ", "")
    //         .Validate(
    //             _mustHaveLengthOf9
    //         );
}




