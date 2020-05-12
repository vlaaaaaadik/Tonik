using NAudio.Wave;
using System;
using System.Windows;
using System.Numerics;
using MathNet.Numerics;
using Accord.Math;
using NAudio.Wave.SampleProviders;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Windows.Media;

namespace audio
{

    public partial class MainWindow : System.Windows.Window
    {
        private static int bufferLength = 4096;

        private static Dictionary<string, Note> notes;

        private static int sampleRate = 44100;

        public static double Gausse(double n, double frameSize)
        {
            var a = (frameSize - 1) / 2;
            var t = (n - a) / (0.5 * a);
            t = t * t;
            return Math.Exp(-t / 2);
        }

        public MainWindow()
        {
            InitializeComponent();
            Mp3FileReader reader1 = new Mp3FileReader("C:\\Users\\vlaad\\Downloads\\mp3.mp3");

            WaveStream stream = WaveFormatConversionStream.CreatePcmStream(reader1);

            WaveFileWriter.CreateWaveFile("C:\\Users\\vlaad\\Downloads\\kek.wav", stream);

        }

        void ReadWave()
        {
            WaveStream reader; 
            if(fileLocation.Text.Contains(".mp3"))
            {
                reader = new Mp3FileReader(fileLocation.Text);
                reader = WaveFormatConversionStream.CreatePcmStream(reader);
            }
            else
            {
                reader = new WaveFileReader(fileLocation.Text);
            }
            ISampleProvider provider = new Pcm16BitToSampleProvider(reader);
            float[] buffer = new float[bufferLength];
            byte[] test = new byte[bufferLength];
            notes = new Dictionary<string, Note>();
            sampleRate = reader.WaveFormat.SampleRate * 4;
            //provider.Read(buffer, 0, buffer.Length);
            
            while (provider.Read(buffer, 0, buffer.Length) != 0)
            {
                detectPitch(buffer); 
            }
            Note[] key = new Note[12];
            notes.Values.CopyTo(key, 0);

            Array.Sort(key);
            Note[] tonal = new Note[7];
            for (int i = 0; i < 7; i++)
            {
                tonal[i] = key[i];
            }
            Tonality tonality = new Tonality(tonal);
            TonalityCoincidence coincidence = new TonalityCoincidence();
            coincidence.calculateCoincidence(tonality);
            Tonality[] tonalities = new Tonality[24];
            coincidence.coincidence.Keys.CopyTo(tonalities, 0);
            System.Windows.Controls.Label label;
            for (int i = 0; i < coincidence.coincidence.Values.Count; i++)
            {
                label = (System.Windows.Controls.Label)A.FindName(tonalities[i].name);
                label.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Convert.ToInt32((255 * (1 - coincidence.coincidence[tonalities[i]]))).ToString("X2") + Convert.ToInt32((255 * coincidence.coincidence[tonalities[i]])).ToString("X2") + "00"));
                Console.WriteLine(tonalities[i].name + " - > "+ coincidence.coincidence[tonalities[i]]*100 + "%" );
            }

                foreach (var j in key)
                {
                    Console.WriteLine(j.name + " - > " + j.magnitude);
                }
        }

        public static void detectPitch(float[] data)
        {
            Complex[] complex = new Complex[data.Length];
 
            int fftPoints = 2;
            while (fftPoints * 2 <= complex.Length)
                fftPoints *= 2;

            for (int i = 0; i < complex.Length; i++)
            {
                complex[i] = new Complex(data[i] * Gausse(i,data.Length), 0.0) ;
            };

            FourierTransform.FFT(complex, FourierTransform.Direction.Forward);

            double[] fftresult = new double[complex.Length / 2] ;

            for (int i = 0; i < fftresult.Length; i++)
            {
                fftresult[i] = complex[i].Magnitude;
            }

            Note note;

            float fftStep = sampleRate / bufferLength;

            for (int i = 0; i < fftresult.Length; i++)
            {
                note = new Note((int)(i * fftStep));

                if (!note.name.Equals("note not found"))
                {

                    if ( notes.ContainsKey( note.ToString() ) )
                        notes[ note.ToString() ].magnitude += fftresult[i];
                    else
                    {
                        note.magnitude += fftresult[i];
                        notes.Add(note.name, note);
                    }
                };
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ReadWave();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "wav (*.wav)|*.wav|All files (*.*)|*.*"; 

            if(dialog.ShowDialog() == true)
            {
                fileLocation.Text = dialog.FileName;
            }

        }
    }
}
