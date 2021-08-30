namespace dotnet_rpg.Models
{
    public class ServiceResponse<T>
    // this class will work as a container class which will hold all the data for the items returned, 
    {
        //T is data to be returned. 
        public T Data  { get; set; } // T data means it will be set to to this data. 
        public bool Success { get; set; } = true; // whether fetching data was sucessful 
        public string Message { get; set; } = null; // this will show whther or not a data was added or not. 
    }
}