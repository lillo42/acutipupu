using Acutipupu.Messages;
using Tutu.Events;

namespace Acutipupu.Extensions;

/// <summary>
/// The key message collection extensions.
/// </summary>
public static class KeyMessageCollectionExtensions
{
    /// <summary>
    /// The get number from the key messages.
    /// </summary>
    /// <param name="messages">The <see cref="KeyMessage"/> collection.</param>
    /// <returns></returns>
    public static int GetNumber(this IEnumerable<KeyMessage> messages)
    {
        var number = 0;
        foreach (var message in messages)
        {
            if (message.Key is not KeyCode.CharKeyCode ch 
                || string.IsNullOrEmpty(ch.Character) 
                || !char.IsDigit(ch.Character[0]))
            {
                break;
            }
            
            number = number * 10 + (ch.Character[0] - '0');
        }

        return number;
    }
}
