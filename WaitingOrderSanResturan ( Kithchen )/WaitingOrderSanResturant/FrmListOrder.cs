using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CardView;
using System.Data.SqlClient;
//using System.Security.Permissions;


using TableDependency;
using TableDependency.SqlClient;
using TableDependency.EventArgs;
using TableDependency.Enums;
using System.Security.Permissions;

using System.Threading;

namespace WaitingOrderKitchen
{
    public partial class FrmListOrder : Form
    {
        public FrmListOrder()
        {
            InitializeComponent();

            try
            {
          ReadConnection();
             Checkfish();


             TimerCallback callback = new TimerCallback(Slider);
             System.Threading.Timer time = new System.Threading.Timer(callback, null, 0, 30000);


            }
            catch (Exception e)
            {
                

            }
                
  

        }

        Table_Watcher tw;
        string ConstrReader ;
        DataTable dtlist = new DataTable();

         public void ReadConnection()
         {

             ConstrReader = ConnectionDB.ConstrReader;


         }

                public void Checkfish()
                {
                    try
                    {
                  

                        if (FLP.Controls.Count == 0)
                        {
                            DataTable dtfish = Func.GetCard(ConstrReader);

                            if (dtfish != null)
                            {
                                int count = dtfish.Rows.Count - 1;

                                string NumFish = string.Empty, TypeFact;
                                bool ReadyFish;
                                DateTime timeorder, _time;
                                int ModateEntezar, TimeFish = 0;
                              
                                for (int index = 0; index <= count; index++)
                                {
                                    TimeFish = 0;
                                        NumFish = dtfish.Rows[index]["ForooshKalaParent_ShomareFish"].ToString();
                                         TypeFact = dtfish.Rows[index]["ForooshKalaParent_TypeFact"].ToString();

                                         string IDFactor = dtfish.Rows[index]["ForooshKalaParent_ID"].ToString();

                                         ReadyFish = (bool)dtfish.Rows[index]["ForooshKalaParent_Ready"];

                                         timeorder = DateTime.Parse(dtfish.Rows[index]["ForooshKalaParent_Time"].ToString());
                                         ModateEntezar = int.Parse(dtfish.Rows[index]["ForooshKalaParent_ModateEntezar"].ToString());
                                         _time = timeorder.AddMinutes(ModateEntezar);
                                   
                                        if (DateTime.Now <= _time)
                                        {
                                            TimeSpan varTime = DateTime.Now - _time;
                                            var intMinutes = varTime.TotalMinutes;

                                            TimeFish = Math.Abs((int)Math.Round(varTime.TotalMinutes));

                                        }

                                        dtfood(IDFactor);

                                        AddCard(NumFish, TimeFish, TypeFact, ReadyFish,dtlist);



                                }


                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //func.WriteErrorLog(ex);
                        //NotificationManager.Show(Me, "خطا در لاگ ثبت گردید", Color.Red, 3000)
                    }

                }

        
        void dtfood(string idfactor)
                {
                    try
                    {
      dtlist = null;
                    DataTable dt = Func.GetFood(ConstrReader, idfactor);
                    dtlist = dt.Copy();
              
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine(e);
                        //throw;
                    }
              
          
                }

                public void AddCard(string numfish, int time, string TypeFact,bool ReadyFish,DataTable dtfood)
                {

                    try
                    {


                            CardView.CardViewKitchen cvd = new CardView.CardViewKitchen();
                            cvd.Name = numfish;
                            cvd.Minute = time;
                            cvd.TimerControl = true;
                            cvd.DataGridFill = dtfood;
                            cvd.CaptionStatusFish = TypeFact;
                            cvd.NumberFish = numfish;

                            if (ReadyFish)
                            {
                                cvd.StopCard();
                            }

                            if (FLP.InvokeRequired)
                            {
  
                                FLP.Invoke((MethodInvoker)delegate()
                                {
                                    AddCard(numfish, time, TypeFact, ReadyFish, dtfood);
                                });

                            }
                            else
                            {
                                FLP.Controls.Add(cvd);
                            }



                        
                    }
                    catch (Exception ex)
                    {
                    //    //func.WriteErrorLog(ex);
                    //    //NotificationManager.Show(Me, "خطا در لاگ ثبت گردید", Color.Red, 3000)
                    }
                }
                public void DeleteCard(string numfish,bool ReadyFish,bool Delivery ,bool Delete)
                {
                    try
                    {


                    if (Delete || Delivery)
                    {

                        foreach (CardView.CardViewKitchen item in FLP.Controls)
                        {
                
                            if (item.Name == numfish)
                            {
                                if (FLP.InvokeRequired)
                                {

                                    FLP.Invoke((MethodInvoker)delegate()
                                    {
                                        DeleteCard(numfish, ReadyFish,Delivery,Delete);
                                    });

                                }
                                else { FLP.Controls.Remove(item);}
                              }
                        }


                    }
                    else
                    {

                        foreach (CardView.CardViewKitchen item in FLP.Controls)
                        {

                            if (item.Name == numfish)
                            {
                                if (FLP.InvokeRequired)
                                {

                                    FLP.Invoke((MethodInvoker)delegate()
                                    {
                                        DeleteCard(numfish, ReadyFish, Delivery, Delete);
                                    });

                                }
                                else {

                                    item.StopCard();
                                    
                                }
                            }
                        }


                    }

                    }
                    catch (Exception ex)
                    {
                        //func.WriteErrorLog(ex);
                        //NotificationManager.Show(Me, "خطا در لاگ ثبت گردید", Color.Red, 3000)
                    }
                }

                private void FrmListOrder_Load(object sender, EventArgs e)
                {
                    try
                    {
            tw = new Table_Watcher(this, FLP);
                    tw.WatchTable();
                    tw.StartTableWatcher();

                    }
                    catch (Exception exception)
                    {
                        
                    }

        
                   
                }

                private void FrmListOrder_FormClosing(object sender, FormClosingEventArgs e)
                {
             
                    try
                    {
                        tw.StopTableWatcher();
                    }
                    catch (Exception exception)
                    {
                        Application.ExitThread();
                    }
                }



                int counter = 1;
                 void Slider(object Status)
                {
                    try
                    {

            
                    switch (counter)
                    {
                  

                        case 1:
                            this.FLP.BackgroundImage = global::WaitingOrderKitchen.Properties.Resources._1;
                            counter++;
                            break;

                        case 2:
                            this.FLP.BackgroundImage = global::WaitingOrderKitchen.Properties.Resources._2;
                            counter++;

                            break;

                        case 3:
                            this.FLP.BackgroundImage = global::WaitingOrderKitchen.Properties.Resources._3;
                            counter++;

                            break;

                        case 4:
                            this.FLP.BackgroundImage = global::WaitingOrderKitchen.Properties.Resources._4;
                            counter++;

                            break;


                        case 5:
                            this.FLP.BackgroundImage = global::WaitingOrderKitchen.Properties.Resources._5;
                            counter = 1;
                            break;

                    }
                    }
                    catch (Exception e)
                    {

                    }
                }

                 private void FLP_DoubleClick(object sender, EventArgs e)
                 {
                     try
                     {
      Application.ExitThread();
                     }
                     catch (Exception exception)
                     {
                         Application.ExitThread();
                     }
               
                 }



    }
}
