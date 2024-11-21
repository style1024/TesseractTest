# TesseractTest
開始自製Tesseract-OCR 模型之前需要先安裝下面三個東西
* [下載Tesseract安裝包](https://github.com/UB-Mannheim/tesseract/wiki)
* [下載jTessBoxEditor訓練工具](https://sourceforge.net/projects/vietocr/files/jTessBoxEditor/)
* [Java虛擬機](https://www.oracle.com/java/technologies/downloads/#java8)
  
安裝步驟可以參考: [https://blog.csdn.net/qq_41030861/article/details/99842136](https://blog.csdn.net/qq_41030861/article/details/99842136)
訓練之前要先抓一些圖片樣本，越多越好有助於提高效率

# 開始訓練
* 打開jTessBoxEditor，點train.bat或是jTessBoxEditor.jar都可以
* 製作圖片樣本，點擊Tools > Merge TIFF，選擇要訓練的樣本
* 檔案名稱輸入num.font.exp0.tif
*  將num.font.exp0.tif複製到Tesseract-OCR安装資料夾底下。
*  生成Box File，打開CMD進入 Tesseract-OCR
*  輸入tesseract.exe num.font.exp0.tif num.font.exp0 batch.nochop makebox 生成的Box檔為num.font.exp0.box，box檔可以為Tesseract辨識文字和其座標。
> ⚠️ 注意: Make Box File命令有一定格式 ，格式如下:
tesseract [lang].[fontname].exp[num].tif [lang].[fontname].exp[num] batch.nochop makebox
lang為語言名稱(可以參考tesseract語言命名方式)，fontname為字體名稱，num為序號可隨便定義。
* 將上一步驟生成的num.font.exp0.box跟tif檔放在一起，這邊我放回原本圖片的地方處理。運行jTessBoxEditor點Box Editer > Open，選擇剛剛建立的num.font.exp0.tif。
> ⚠️ 注意: 這邊建議不要放在Tesseract-OCR這邊，他這個資料夾有時候會有讀寫問題，如果可以解決就放，不行的話就放回你放圖片的資料夾也行
* 可以看到畫面上有些識別位置和文字都不準確，可以使用該工具進行位置矯正和文字修改。必須把錯誤修正不然之後訓練出來的模型會是錯的。
* 建立一個font_properties檔案，先用txt檔裡面 打上font 0 0 0 0 0 (這裡是在表示字體不是粗體或是斜體等等，文件格式如: <fontname> <italic> <bold> <fixed> <serif> <fraktur>)，之後再把txt副檔名拿掉即可，先前的tif和box檔也順便一起放進Tesseract-OCR
* 在樣本和font_properties檔案所在的資料夾執行CMD指令，指令如下:
```
echo Run Tesseract for Training..
tesseract.exe num.font.exp0.tif num.font.exp0 nobatch box.train
echo Compute the Character Set..
unicharset_extractor.exe num.font.exp0.box
mftraining -F font_properties -U unicharset -O num.unicharset num.font.exp0.tr
echo Clustering..
cntraining.exe num.font.exp0.tr
echo Rename Files..
rename normproto num.normproto
rename inttemp num.inttemp
rename pffmtable num.pffmtable
rename shapetable num.shapetable
echo Create Tessdata..
combine_tessdata.exe num.
```
* 最後就會在相同資料夾底下發現生成好的traineddata，放到程式底下即可使用
