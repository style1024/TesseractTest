using Tesseract;

// Initialize Tesseract Engine
using var engine = new TesseractEngine(@"\tessdata", "eng", EngineMode.TesseractAndLstm);
engine.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"); // Only recognize numbers and letters
engine.SetVariable("page_seg_mode", "3"); // Auto layout detection

// Image paths
string imageFolder1 = "\\Images\\cap_processed.jpeg";

// Process the first image
string text;
using (var img = Pix.LoadFromFile(imageFolder1))
using (var page = engine.Process(img))
{
    text = page.GetText();
    text = text.Replace("\n", "").Trim();
    Console.WriteLine($"Processed Image Recognition Result: {text}");
}


// Wait for user input before exiting
Console.ReadLine();