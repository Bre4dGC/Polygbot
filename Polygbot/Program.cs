﻿using Polygbot;

internal class Program
{
    private static async Task Main(string[] args)
    {
        BotService botService = new BotService();
        await botService.StartAsync();
    }
}