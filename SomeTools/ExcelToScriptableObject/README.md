# ExcelToScriptableObject

## 提示

该package是为了导入大量数据做的 使用不是很方便 需要自己改动写ScriptableObject赋值代码适配自己的需求。

## 导入Package

Assets/Import Package/Custom Package 选择包导入

## 使用

首先找到脚本**ExcelToScriptableObject**，对方法GetScriptableObjects初始化物体赋值进行修改，每个人初始化的物体属性不同所以需要根据自己需要进行修改。package中有示例。可看后面代码详解。

Unity上方导航栏中找到ExcelTool，点击选择Import，出现面板如下图左一所示

选择需要生成的ScriptableObject，出现右图所示界面，选择想要生成的ScriptableObject

再选择excel，**非常不建议使用xls**，会出现无法初始化list等问题。

**重复导入确保删除之前的生成的ScriptableObject，否则会出现问题，如导入的数据未改变等**。

解决方案：去文件管理器删除再重新生成

<img src="C:\Users\地下城堡\AppData\Roaming\Typora\typora-user-images\image-20220729131936393.png" style="zoom:50%;" /><img src="C:\Users\地下城堡\AppData\Roaming\Typora\typora-user-images\image-20220729132105092.png" alt="image-20220729132105092" style="zoom:50%;" />

## 代码详解

### 重写赋值部分

`DataRowCollection`可视为二维string数组

```c#
DataRowCollection data = ReadExcel(excelpath, out int col, out int row, type);

Item item = ScriptableObject.CreateInstance<Item>();//创建脚本实例

//给数据赋值 data[i][j]表示为excel第i行第j个格子里的内容，tostring转换为string类型 
//从string转int float 可用 int.TryParse float.Parse 将string类型转int float
item.itemName = data[i][0].ToString();
int.TryParse(data[i][1].ToString(), out item.times);
item.list = new List<string>(data[i][2].ToString().Split(';'));

//保存第一个参数为gameobject，需要保存的物体，第二个为保存地址 注意包括保存物体的名字
AssetDatabase.CreateAsset(item, "Assets/Items/item" + i + ".asset");
```
### 窗口

```c#
public class ExcelWindow : EditorWindow
    {
        public ScriptableObject source;

        [MenuItem("ExcelTool/Import")]
        public static void OnInit()
        {
            //创建窗口 泛型参数为创建窗体的脚本 需要继承EditorWindow 类似挂载在物体上的脚本需要继承MonoBehaviour
            //第一个参数string:标题 第二个参数bool:是否聚焦 
            GetWindow<ExcelWindow>("导入excel", true);
        }

    	//绘制UI
        public void OnGUI()
        {
            //开始一行 和EndHorizontal配套使用
            GUILayout.BeginHorizontal();
            //显示一个标签
            GUILayout.Label("Import Excel");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Excel/根据excel后缀选");
            GUILayout.EndHorizontal();
            
            //第一个参数：显示一个字段前的标签 第二个参数:字段显示的对象 第三个参数:可选择的类型 第四个参数：是否可选择场景(Scene)中的对象
            source = (ScriptableObject)EditorGUILayout.ObjectField("选择需要生成的ScriptalbeObject脚本", source, typeof(ScriptableObject), false);

            //创建button 点击button变为true 运行里面的代码
            if (GUILayout.Button("选择Excel/后缀为xlsx"))
            {
                //打开一个文件夹 第一个参数：title 第二个：打开的路径 第三个：可选择的文件种类 适配后缀
                string filePath = EditorUtility.OpenFilePanel("选择Excel/后缀为xlsx", Application.dataPath, "*xlsx");
                ExcelToScriptableObject.GetScriptableObjects(filePath, source,ExcelType.xlsx);
                //Debug.LogError("filePath"+filePath);
            }
            if (GUILayout.Button("选择Excel/后缀为xls(不推荐)"))
            {
                string filePath = EditorUtility.OpenFilePanel("选择Excel/后缀为xls", Application.dataPath, "*xls");
                ExcelToScriptableObject.GetScriptableObjects(filePath,source, ExcelType.xls);

            }
        }
    }
```

## 使用过程中可能出现的问题

**不建议使用xls，大概出问题，做的时候用xls测试生成list完全无法生成成功 甚至string 赋值也有问题 可能是我没弄懂 总之xlsx测试就完全没问题**

- 重新导入文件值没变化

  删除之前生成的ScriptableObject 再重试

- 选择ScriptableObject 时没有我想要生成的

  使用前需要先创建一个ScriptableObject实例，指右键Create，不是ScriptableObject C#脚本

## 参考

[Unity实用功能之读写Excel表格｜ 8月更文挑战 - 掘金 (juejin.cn)](https://juejin.cn/post/6991464138782277645)

[(31条消息) Unity Excel 文件读取和写入_PassionY的博客-CSDN博客_unity读取excel文件](https://blog.csdn.net/yupu56/article/details/50580277)

[Unity知识记录--自动处理大量数据导入，生成ScriptObject（制作UI界面） - 哔哩哔哩 (bilibili.com)](https://www.bilibili.com/read/cv12931265?from=search&spm_id_from=333.337.0.0)