namespace Project3
{
    enum RespondType
    {
        Accept,
        Drop
    }
    class Respond
    {
        public RespondType RespondType { get; set; }
        public Request Request { get; set; }
    }
}