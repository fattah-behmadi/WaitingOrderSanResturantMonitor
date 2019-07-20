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

namespace WaitingOrderSalon
{
    public partial class FrmListOrder : Form
    {
        public FrmListOrder()
        {
            InitializeComponent();
            ReadConnection();
             Checkfish();


             TimerCallback callback = new TimerCallback(Slider);
             System.Threading.Timer time = new System.Threading.Timer(callback, null, 0, 30000);
        }

        Table_Watcher tw;
        string ConstrReader ;
   

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

                                for (int index = 0; index <= count; index++)
                                {
                                    string NumFish = dtfish.Rows[index]["ForooshKalaParent_ShomareFish"].ToString();
                                    string Customer = dtfish.Rows[index]["ForooshKalaParent_Tahvilgirande"].ToString();
                                  
                                    string TypeFact = dtfish.Rows[index]["ForooshKalaParent_TypeFact"].ToString();
                                    bool ReadyFish =(bool) dtfish.Rows[index]["ForooshKalaParent_Ready"];

                                    DateTime timeorder =DateTime.Parse(dtfish.Rows[index]["ForooshKalaParent_Time"].ToString());
                                    int ModateEntezar = int.Parse(dtfish.Rows[index]["ForooshKalaParent_ModateEntezar"].ToString());

                                    DateTime _time = timeorder.AddMinutes(ModateEntezar);

                                    int TimeFish = 0;

                                    if (DateTime.Now <= _time)
                                    {
                                        TimeSpan varTime = DateTime.Now - _time;
                                        var intMinutes = varTime.TotalMinutes;

                                        TimeFish = Math.Abs((int)Math.Round(varTime.TotalMinutes));

                                    }




                                    AddCard(NumFish, Customer, TimeFish, TypeFact,ReadyFish);

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
                public void AddCard(string numfish, string namecustomer, int time, string TypeFact,bool ReadyFish)
                {

                    try
                    {

                        if (TypeFact == "داخل سالن بالا" || TypeFact == "داخل سالن پایین" || TypeFact == "مراجعه داخل سالن")
                        {

                            CardView.CardViewSan cvd = new CardView.CardViewSan();
                            cvd.Name = numfish;
                            cvd.CaptionCustomer = namecustomer;
                            cvd.Minute = time;
                            cvd.CaptionFish = "فیش :" + numfish;
                            cvd.TimerControl = true;
                            if (ReadyFish)
                            {
                                cvd.StopCard();
                            }

                            if (FLP.InvokeRequired)
                            {
  
                                FLP.Invoke((MethodInvoker)delegate()
                                {
                                    AddCard(numfish, namecustomer, time, TypeFact, ReadyFish);
                                });

                            }
                            else
                            {
                                FLP.Controls.Add(cvd);
                            }



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

                        foreach (CardView.CardViewSan item in FLP.Controls)
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

                        foreach (CardView.CardViewSan item in FLP.Controls)
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
                            this.FLP.BackgroundImage = global::WaitingOrderSalon.Properties.Resources._1;
                            counter++;
                            break;

                        case 2:
                            this.FLP.BackgroundImage = global::WaitingOrderSalon.Properties.Resources._2;
                            counter++;

                            break;

                        case 3:
                            this.FLP.BackgroundImage = global::WaitingOrderSalon.Properties.Resources._3;
                            counter++;

                            break;

                        case 4:
                            this.FLP.BackgroundImage = global::WaitingOrderSalon.Properties.Resources._4;
                            counter++;

                            break;


                        case 5:
                            this.FLP.BackgroundImage = global::WaitingOrderSalon.Properties.Resources._5;
                            counter = 1;
                            break;



                    }
                  
      }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
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
