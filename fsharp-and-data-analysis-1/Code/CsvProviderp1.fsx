// Тип-провайдер
type Stocks = CsvProvider<
    "../data/MSFT.csv",                    // образец данных
    ResolutionFolder=__SOURCE_DIRECTORY__   // где искать файл
>

// Загружаем и кэшируем данные
let msft =
    Stocks
        // Работает лениво
        .Load(__SOURCE_DIRECTORY__ + "/data/MSFT.csv")
        // Выгузило в RAM весь файл
        .Cache()