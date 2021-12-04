using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class NewScript : MonoBehaviour {
    private void Start() {
        // Mixed Datatype Collection:
        IList mixedList = new ArrayList();
        mixedList.Add(0);
        mixedList.Add("One");
        mixedList.Add("Two");
        mixedList.Add(3);
        mixedList.Add(new Student() {StudentID = 1, StudentName = "Bill"});
        
        // TypeOf Clause in Query Syntax (Filter All String Variables):
        var stringResult = from s in mixedList.OfType<string>()
            select s;
        
        // TypeOf Clause in Query Syntax (Filter All Integer Variables):
        var intResult = from s in mixedList.OfType<int>()
            select s;

        foreach (var s in intResult) {
            Debug.Log(s);
        }

    }

    class Student
    {
        public int StudentID { get; set; }
        public String StudentName { get; set; }
        public int Age { get; set; }
    }
}





