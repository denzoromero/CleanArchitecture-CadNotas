namespace CleanCadNotas.Models
{
    public record StatusMessageViewModel(bool IsSuccess, IEnumerable<string> Messages)
    {
        //public bool IsSuccess { get; set; }

        //public IEnumerable<string> Messages { get; set; } = Enumerable.Empty<string>();

        //public StatusMessageViewModel(bool isSuccess, IEnumerable<string> messages)
        //{
        //    IsSuccess = isSuccess;
        //    Messages = messages;
        //}

    }

}
