using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class NewScript : MonoBehaviour {
    private void Start() {
        
        // Define Student List:
        IList<Student> studentList = new List<Student>() { 
            new Student() { StudentID = 1, StudentName = "John", StandardID =1 },
            new Student() { StudentID = 2, StudentName = "Moin", StandardID =1 },
            new Student() { StudentID = 3, StudentName = "Bill", StandardID =2 },
            new Student() { StudentID = 4, StudentName = "Ram" , StandardID =2 },
            new Student() { StudentID = 5, StudentName = "Ron"  } 
        };

        // Define Standard List:
        IList<Standard> standardList = new List<Standard>() { 
            new Standard(){ StandardID = 1, StandardName="Standard 1"},
            new Standard(){ StandardID = 2, StandardName="Standard 2"},
            new Standard(){ StandardID = 3, StandardName="Standard 3"}
        };
        
        // Join Query in C#:
        var innerJoin = studentList.Join( // outer sequence
            standardList, // inner sequence
            student => student.StandardID, // outer key
            standard => standard.StandardID, // inner key
            (student, standard) => new { // projection result
                StudentName = student.StudentName,
                StandardName = standard.StandardName
            });

        foreach (var s in innerJoin) {
            Debug.Log("Student Name: " + s.StudentName + " Standard ID: " + s.StandardName);
        }
    }
    
    public class Student{ 
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int StandardID { get; set; }
    }

    public class Standard{ 
        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }
    
    /*
    * Both Method and Query methods produce the following results:
    * Student Name: John, Standard ID: 1
    * Student Name: Moin, Standard ID: 1
    * Student Name: Bill, Standard ID: 2
    * Student Name: Ram, Standard ID: 2
    */
}





