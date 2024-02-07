using WebTask.Models;

namespace WebTask.Sevices;

public class DbService : IDbService
{
    private string path = @"Data\dane.csv";
    HashSet<Student> studentsSet = new(new StudentComparator());
    Student student;
    
    public DbService()
    {
        var fileIn = new FileInfo(path);

        using (var stream1 = new StreamReader(fileIn.OpenRead()))
        {
            string line;
            while ((line = stream1.ReadLine()) != null)
            {
                StringToStudentsSet(line);
            }

        }
    }
    
    public void StringToStudentsSet(string values)
    {
       
        string[] studentData = values.Split(",");
                    
            if (studentData.Length < 9 | studentData.Length > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(values),"Too many or too few arguments");
            }

            student = new Student
            {
                index = studentData[2],
                name = studentData[0],
                surname = studentData[1],
                birhdate = studentData[3],
                email = studentData[6],
                mothersName = studentData[7],
                fathersName = studentData[8],
                studies  = studentData[4],
                mode = studentData[5],
                            
            };
            AddStudent(student);
    }

    public bool AddStudent(Student student)
    {
        var values = student.ToString();
        var studentData = values.Split(",");
        //bool validStudent = true;

        for (int i = 0; i < studentData.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(studentData[i]))
            {
                throw new ArgumentNullException(nameof(student), "The columns can't be empty");
            }
        }

        if (!studentsSet.Add(student))
        {
            throw new ArgumentException("Student already exists in database");
        }
        return true;
    }

    public HashSet<Student> AllStudents()
    {
        return studentsSet;
    }

    public void updateStudentSet(HashSet<Student> newSet)
    {
        studentsSet = newSet;
        using var streamWriter = new StreamWriter(path);
        foreach (var student in studentsSet)
            streamWriter.WriteLine(student);
    }
}
