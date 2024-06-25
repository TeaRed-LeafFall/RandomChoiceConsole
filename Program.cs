var hiderandom = false;

var stringList = new List<string>();
Console.WriteLine("欢迎使用 RandomChoice！");
Console.WriteLine("编辑文本时提示: \n\tEnd输入结束 \n\tClear清空列表 \n\t使用;分割可以一次输入多个 \n\tH隐藏或显示抽取过程");

//输入文本
newinput:
Console.Write("请输入文本或命令：");
var input = Console.ReadLine();

if ( input == "End" ) goto asknumber;

if ( input == "H" )
{
    if(hiderandom){
        hiderandom = false;
        Console.WriteLine("抽取过程已显示，按H隐藏抽取过程");
    }else{
        hiderandom = true;
        Console.WriteLine("抽取过程已隐藏，按H显示抽取过程");
    }
    goto newinput;
}
if ( input == "Clear" )
{
    stringList.Clear();
    goto newinput;
}

if ( string.IsNullOrEmpty(input) )
{
    Console.WriteLine("输入不能为空，请重新输入！");
    goto newinput;
}

//如果字符串中具有;则分割，列表中有的不添加
if ( input.Contains(";") )
{
    var inputlist = input.Split(';');
    foreach ( var item in inputlist )
    {
        if ( !stringList.Contains(item) )
        {
            stringList.Add(item);
        }
    }
}else{

    //判断是否在列表中重复，如果不重复不为空则添加
    if ( !stringList.Contains(input) )
    {
        stringList.Add(input);
    }else{
        Console.WriteLine("输入重复，请重新输入！");
    }
}

goto newinput;

asknumber:
if(stringList.Count == 0)
{
    Console.WriteLine("输入为空，请重新输入！");
    goto newinput;
}
Console.WriteLine();
Console.WriteLine("当前共有 {0} 条输入，请输入需要随机的次数：", stringList.Count);
//这个字典用于记录抽取过的文本，key为原本文本，value为到的抽取次数
Dictionary<string,int> extracted=new();
var input2 = Console.ReadLine();

if ( !int.TryParse(input2, out var number) )
{
    Console.WriteLine("输入错误，请重新输入！");
    goto asknumber;
}

if ( number == 0 )
{
    Console.WriteLine("输入不能为0，请重新输入！");
    goto asknumber;
}

startrandom:

Console.WriteLine();
Console.WriteLine("抽取开始！！！！！");
Console.WriteLine();
Console.WriteLine("『当前共有 {0} 条输入，需要抽取 {1} 次！』", stringList.Count, number);
Console.WriteLine();




//随机抽取
for ( var i = 0; i < number; i++ )
{

    var random = new Random();

    var index = random.Next(0, stringList.Count);

    var result = stringList[index];
    
    //判断是否抽取过，如果抽取过则增加抽取到的次数
    if ( extracted.ContainsKey(result) )
    {
        extracted[result]++;
    }else{
        extracted.Add(result, 1);
    }
    if(!hiderandom) Console.WriteLine("\t第 {0} 次抽取 - 随机数是 {1} ，抽取结果是 『{2}』",i+1, index, result);
}
Console.WriteLine();
Console.WriteLine("抽取结束！！！！！");
Console.WriteLine();
Console.WriteLine();
//按照抽取的次数排序输出
var sorted = extracted.OrderByDescending(x => x.Value).ToList();
Console.WriteLine("！！！！！最后结果！！！！！");
foreach ( var item in sorted )
{
    Console.WriteLine("\t『{0}』 被抽取到的次数是：{1} 占比:{2}%", item.Key, item.Value, (double)item.Value / number * 100);
}
//输出方差，中位数，平均数，最大值，最小值
Console.WriteLine("！！！！！统计结果计算！！！！！");
Console.WriteLine();
Console.WriteLine("\t方差： {0} ", sorted.Average(x => x.Value) - number);
Console.WriteLine("\t中位数： {0} ", sorted[(int)Math.Floor((sorted.Count - 1) / 2.0)]);
Console.WriteLine("\t平均数： {0} ", sorted.Average(x => x.Value));
Console.WriteLine("\t最大值： {0} ", sorted.Max(x => x.Value));
Console.WriteLine("\t最小值： {0} ", sorted.Min(x => x.Value));
Console.WriteLine();
Console.WriteLine();
//询问用户是否继续
Console.WriteLine();
Console.WriteLine("是否继续抽取？(Y继续，R使用相同次数再次抽取，其他退出)");
var input3 = Console.ReadLine();
if(input3==null)return;
if ( input3.ToUpper() == "Y" ) {
    askback:
    Console.Write("输入Y则使用原来的项继续抽取，输入N返回编辑[Y/N]");
    input3 = Console.ReadLine();

    if(input3==null)goto askback;
    
    switch ( input3.ToUpper() )
    {
        case "Y":
            goto asknumber;
        case "N":
            goto newinput;
        default:
            Console.WriteLine("只能输入Y或N，请重新输入！");
            goto askback;
    }
    
}
else{
    if(input3.ToUpper() == "R"){
        goto startrandom;
    }
    else{
        Console.WriteLine("程序结束！");
    }
}