using System.ComponentModel.DataAnnotations;

namespace ValidationLib;

public interface IValidable { }

public interface IValidableObjectResult<T>
        where T : IValidable
{
  bool IsValid { get; }
  IEnumerable<ValidationResult> Result { get; }
  T Instance { get; }
}

public sealed class ValidableObjectResult<T> : IValidableObjectResult<T>
        where T : IValidable
{
  public bool IsValid { get; set; }
  public required IEnumerable<ValidationResult> Result { get; set; }
  public required T Instance { get; set; }
}

public static class ValidableObjectHelper
{
  /// <summary>
  /// Validates the argument
  /// </summary>
  public static IValidableObjectResult<T> Validate<T>(T arg) where T : IValidable
  {
    var context = new ValidationContext(arg);
    var errors = new List<ValidationResult>();

    return Validator.TryValidateObject(arg, context, errors)
      ? new ValidableObjectResult<T>()
      {
        Instance = arg,
        IsValid = true,
        Result = [],
      }
      : new ValidableObjectResult<T>()
      {
        Instance = arg,
        IsValid = false,
        Result = errors.AsEnumerable(),
      };
  }
}
