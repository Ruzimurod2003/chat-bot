using ChatBot;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyCareerBot.Controllers;

[ApiController]
[Route("/")]
public class BotController : ControllerBase
{
    private readonly ITelegramBotClient _botClient;
    private readonly ApplicationContext _context;

    public BotController(ITelegramBotClient botClient, ApplicationContext context)
    {
        _botClient = botClient;
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        if (update == null) return BadRequest();

        try
        {
            ReplyKeyboardMarkup markup = new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton(".NET"),
                    new KeyboardButton("JavaScript"),
                    new KeyboardButton("PHP"),
                }
            });
            if (update.Message != null)
            {
                if (update.Message.Text == "/start")
                {
                    ReplyKeyboardMarkup requestReplyKeyboard = new(new[]
                    {
                        KeyboardButton.WithRequestContact("Send my phone number"),
                    });
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Welcome to the bot, send your telephone contact",
                    replyMarkup: requestReplyKeyboard);
                }
                else if (update.Message.Text == "Back to main")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "In which language do you answer questions?",
                    replyMarkup: markup);
                }
                else if (update.Message.Text == ".NET")
                {
                    ReplyKeyboardMarkup markupDegree = new ReplyKeyboardMarkup(new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            new KeyboardButton(".NET Beginner")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton(".NET Intermediate")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton(".NET Advanced")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton("Back to main")
                        }
                    });
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "At what level do you want the tests?",
                    replyMarkup: markupDegree);
                }
                else if (update.Message.Text == "JavaScript")
                {
                    ReplyKeyboardMarkup markupDegree = new ReplyKeyboardMarkup(new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            new KeyboardButton("JavaScript Beginner")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton("JavaScript Intermediate")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton("JavaScript Advanced")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton("Back to main")
                        }
                    });
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "At what level do you want the tests?",
                    replyMarkup: markupDegree);
                }
                else if (update.Message.Text == "PHP")
                {
                    ReplyKeyboardMarkup markupDegree = new ReplyKeyboardMarkup(new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            new KeyboardButton("PHP Beginner")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton("PHP Intermediate")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton("PHP Advanced")
                        },
                        new KeyboardButton[]
                        {
                            new KeyboardButton("Back to main")
                        }
                    });
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "At what level do you want the tests?",
                    replyMarkup: markupDegree);
                }
                else if (update.Message.Text == ".NET Beginner")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");

                    var questions = await AsistantService.CreateTestForLanguages(".NET", "Beginner");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Text == ".NET Intermediate")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");
                    var questions = await AsistantService.CreateTestForLanguages(".NET", "Intermediate");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Text == ".NET Advanced")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");
                    var questions = await AsistantService.CreateTestForLanguages(".NET", "Advanced");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Text == "JavaScript Beginner")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");
                    var questions = await AsistantService.CreateTestForLanguages("JavaScript", "Beginner");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Text == "JavaScript Intermediate")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");
                    var questions = await AsistantService.CreateTestForLanguages("JavaScript", "Intermediate");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Text == "JavaScript Advanced")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");
                    var questions = await AsistantService.CreateTestForLanguages("JavaScript", "Advanced");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Text == "PHP Beginner")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");
                    var questions = await AsistantService.CreateTestForLanguages("PHP", "Beginner");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Text == "PHP Intermediate")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");
                    var questions = await AsistantService.CreateTestForLanguages("PHP", "Intermediate");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Text == "PHP Advanced")
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Please wait while your questions are being prepared...");
                    var questions = await AsistantService.CreateTestForLanguages("PHP", "Advanced");
                    foreach (var question in questions)
                    {
                        await _botClient.SendPollAsync(
                            chatId: update.Message.Chat.Id,
                            question: question.Message,
                            options: new[]
                            {
                            question.AnswerA,
                            question.AnswerB,
                            question.AnswerC,
                            question.AnswerD,
                            },
                            type: Telegram.Bot.Types.Enums.PollType.Quiz,
                            correctOptionId: question.RightAnswer
                        );
                    }
                }
                else if (update.Message.Contact != null)
                {
                    string phoneNumber = update.Message.Contact.PhoneNumber;

                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "In which language do you answer questions?",
                    replyMarkup: markup);
                }
                else
                {
                    await _botClient.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "In which language do you answer questions?",
                    replyMarkup: markup);
                }
            }
        }
        catch (Exception ex)
        {
            await _botClient.SendTextMessageAsync(
                update.Message.Chat.Id,
                ex.Message,
            replyMarkup: new ReplyKeyboardRemove());
        }
        return Ok();
    }
    [HttpGet]
    public string Get()
    {
        return "Telegram bot was started";
    }
}