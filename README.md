# Meerkat

<center>
    <img src="meerkat.jpg" alt="alt text" width="250">
</center>

⚠️ Work in progress! ⚠️

## Описание

Приложение для генерации текста из шаблонов (шаблонизатор), умеющее изменять форму слова. Частный случай применения - генерация аргументов против X, за Y и т.д.; для чего, собственно, и делалось.

## Использование

Слева - рабочая область, в которой сверху находится окно редактирования, а снизу расположено окно редактирвания переменных для шаблона.

Справа окно вывода.

Вообще, UI довольно простой, запутаться сложно)

## Шаблоны

### Создание шаблонов

Общий формат таков: **[ X | form modifier]**. Программа заменит такую запись на подходящую, основываясь на **form** и **modifier**.

Символ **|**, вообще говоря, не обязателен (то есть, **[X | form]** и **[X form]** эквивалентны), но он помогает чётко отделить используемую переменную от формы, в которую необходимо её поставить. Рекомендуется для бóльшей читабельности всё же ставить **|**.

#### **Префиксы**

Префикс **^** указывает на необходимость установки первой буквы слова в верхний регистр.

Префикс **^** - на установку всех букв в верхний регистр.

#### **Переменные**

Название переменной должно соответствовать правилам:

*  Переменные состоят из прописных букв латинского алфавита, символа подчёркивания '**_**' и тире '**-**'.

Допускается передача в аргументах "лишних" переменных.

#### **Обработка слова**, **form**

На месте **form** может быть одна из строк, представляющих формы слов, соответствующая таблице (символы в фигурных скобках *{}* являются опциональными):

Используемая строка | Форма слова | Пояснение | Пример | Для каких частей речи 
------------------- | ----------- | --------- | ------ | ---------------------
*пустая строка* |  | то же, что и **именительный** падеж | | все
**about** |  | **предложный** падеж, но с подходящим предлогом *о/об* | об интернете / о матче | существительные
**обо** |  | то же, что и **about** | | существительные
**имен{ительный}** | именительный падеж | Кто? Что? | хомяк ест | существительные, прилагательные
**родит{ельный}** | родительный падеж | Кого? Чего? | у нас нету хомяка | существительные, прилагательные
**дате{льный}** | дательный падеж | Кому? Чему? | сказать хомяку спасибо | существительные, прилагательные
**вини{тельный}** | винительный падеж | Кого? Что? | хомяк читает книгу | существительные, прилагательные
**твор{ительный}** | творительный падеж | Кем? Чем? | зерно съедено хомяком | существительные, прилагательные
**предл{ожный}** | предложный падеж | О ком? О чём? и т.п. | хомяка несут в корзинке | существительные, прилагательные

#### **Модификаторы** (**modifier**)

Доступен модификатор **+**, указывающий на множественное число. Например:

[X+], [X | +] или [X| именительный+] при **X**=*яблоко* будет означать *яблоки*.

Для прилагательных доступен модификатор рода; варианты в таблице:

Используемая строка | Род 
------------------- | ---
F | женский
M | мужской
N | средний
FA | женский, одушевлённое
MA | мужской, одушевлённое
NA | средний, одушевлённое
MAFA | общий род, одушевлённое

### Пример

При **X** = **кот** и **Y** = **молоко**, **Z** = **испорченное**:

* Выражение **[X|именительный]овасия**, **[X|имен]овасия**, **[X|]** или **[X]овасия** будет заменено на **котовасия**;

* Выражение **[X|form]** может иметь между словами сколько угодно пробелов, к примеру **[  X |form      ]**. Разницы после обработки не будет;

* **^[Z N]** **[Y] для [X | родительный]** будет заменено на **Испорченное молоко для кота**.

## Warning

1) **Works only with russian language**;
2) Некоторые слова не доступны для склонения в некоторые формы. Work in progress.
