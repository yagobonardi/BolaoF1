public record CreateUserDTO
{
    public string Name { get; set; } = string.Empty;

    public string Mail { get; set; } = string.Empty;

    public string CityState { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}