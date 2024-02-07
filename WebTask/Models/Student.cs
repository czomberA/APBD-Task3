namespace WebTask.Models;

public class Student
{
    public string name { get; set; }
    public string surname { get; set; }
    public string index { get; set; }
    public string birhdate { get; set; }
    public string email { get; set; }
    public string studies { get; set; }
    public string mode { get; set; }
    public string fathersName { get; set; }
    public string mothersName { get; set; }
        
    public override string ToString()
    {
        return $"{name},{surname},{index},{birhdate},{studies},{mode},{email},{fathersName},{mothersName}";
    }
}