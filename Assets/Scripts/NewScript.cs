using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class NewScript : MonoBehaviour {
    private void Start() {
        
        // Define Student List:
        IList<Student> studentList = new List<Student>() { 
            new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 } 
        };


        // GroupBy Query in Query Syntax [group by age]:
        var groupByQuery = from s in studentList
            group s by s.Age;
        
        // Iterate through each group:
        foreach (var ageGroup in groupByQuery) {
            Debug.Log("Age Group Key:" + ageGroup.Key); // Each group has a key

            foreach (var s in ageGroup) {
                Debug.Log("Student Name: " + s.StudentName);
            }
        }
    }

    class Student
    {
        public int StudentID { get; set; }
        public String StudentName { get; set; }
        public int Age { get; set; }
    }
    
    /*
    * This code produces the following results:
    * AgeGroup: 18
     * StudentName: John
     * StudentName: Bill
    * Age Group 21:
     * StudentName: Steve
     * StudentName: Abram
    * Age Group 20:
     * Ram
    */
}





