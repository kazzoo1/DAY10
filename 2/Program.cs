﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Автомобиль : IComparable<Автомобиль>
{
    public string Госномер { get; private set; }
    public string Цвет { get; private set; }
    public string ФамилияВладельца { get; private set; }
    public bool НаСтоянке { get; set; }

    public Автомобиль(string госномер, string цвет, string фамилияВладельца, bool настоянке)
    {
        Госномер = госномер;
        Цвет = цвет;
        ФамилияВладельца = фамилияВладельца;
        НаСтоянке = настоянке;
    }

    public int CompareTo(Автомобиль other)
    {
        return Госномер.CompareTo(other.Госномер);
    }

    public override string ToString()
    {
        return $"Автомобиль: {Цвет}, госномер {Госномер}, владелец {ФамилияВладельца}, " +
               $"находится на стоянке: {(НаСтоянке ? "Да" : "Нет")}.";
    }

    public static bool operator >(Автомобиль a1, Автомобиль a2)
    {
        return a1.CompareTo(a2) > 0;
    }

    public static bool operator <(Автомобиль a1, Автомобиль a2)
    {
        return a1.CompareTo(a2) < 0;
    }
}

public class Автостоянка
{
    private List<Автомобиль> автомобили = new List<Автомобиль>();

    public void ДобавитьАвтомобиль(Автомобиль автомобиль)
    {
        автомобили.Add(автомобиль);
    }

    public Автомобиль НайтиАвтомобильПоГосномеру(string госномер)
    {
        return автомобили.FirstOrDefault(а => а.Госномер == госномер);
    }

    public IEnumerable<Автомобиль> ПолучитьВсеНаСтоянке()
    {
        return автомобили.Where(а => а.НаСтоянке);
    }

    public IEnumerable<Автомобиль> ПолучитьВсеНеНаСтоянке()
    {
        return автомобили.Where(а => !а.НаСтоянке);
    }

    public Автомобиль this[int номерМеста]
    {
        get { return автомобили.Count > номерМеста ? автомобили[номерМеста] : null; }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Автостоянка автостоянка = new Автостоянка();

        автостоянка.ДобавитьАвтомобиль(new Автомобиль("А123ВС77", "Красный", "Иванов", true));
        автостоянка.ДобавитьАвтомобиль(new Автомобиль("В456ДЕ89", "Синий", "Петров", false));
        автостоянка.ДобавитьАвтомобиль(new Автомобиль("С789ФГ01", "Зеленый", "Сидоров", true));

        // Поиск автомобиля по госномеру
        var авто = автостоянка.НайтиАвтомобильПоГосномеру("А123ВС77");
        Console.WriteLine(авто != null ? авто.ToString() : "Автомобиль не найден.");

        // Вывод списка присутствующих на стоянке автомобилей
        Console.WriteLine("\nПрисутствующие на стоянке автомобили:");
        foreach (var присутствующийАвто in автостоянка.ПолучитьВсеНаСтоянке())
        {
            Console.WriteLine(присутствующийАвто);
        }

        // Вывод списка отсутствующих на стоянке автомобилей
        Console.WriteLine("\nОтсутствующие на стоянке автомобили:");
        foreach (var отсутствующийАвто in автостоянка.ПолучитьВсеНеНаСтоянке())
        {
            Console.WriteLine(отсутствующийАвто);
        }

        // Доступ к автомобилю по номеру места
        int номерМеста = 1;
        Автомобиль автомобильНаМесте = автостоянка[номерМеста];
        if (автомобильНаМесте != null)
        {
            Console.WriteLine($"\nАвтомобиль на месте {номерМеста}: {автомобильНаМесте}");
        }
        else
        {
            Console.WriteLine($"\nНа месте {номерМеста} автомобиля нет.");
        }
    }
}