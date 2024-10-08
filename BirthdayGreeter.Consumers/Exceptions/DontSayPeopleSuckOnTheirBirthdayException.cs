using System;

public class DontSayPeopleSuckOnTheirBirthdayException : Exception
{
    public DontSayPeopleSuckOnTheirBirthdayException(string message) : base(message) { }
}