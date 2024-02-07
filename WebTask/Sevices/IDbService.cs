using WebTask.Models;

namespace WebTask.Sevices;

public interface IDbService
{
    public void StringToStudentsSet(string values);
    public bool AddStudent(Student student);
    public HashSet<Student> AllStudents();
    public void updateStudentSet(HashSet<Student> newSet);
}