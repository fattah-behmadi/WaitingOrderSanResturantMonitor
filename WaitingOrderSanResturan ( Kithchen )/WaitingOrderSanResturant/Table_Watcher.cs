
using System.Drawing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDependency;
using TableDependency.Enums;
using WaitingOrderSanResturant;
using TableDependency.SqlClient;
using WaitingOrderKitchen;
using System.Data;

using System.Threading;

   public class Table_Watcher
    {


       FrmListOrder Frm;

       FlowLayoutPanel FLP;

       public Table_Watcher(FrmListOrder frm, FlowLayoutPanel Flp)
       {

           Form f2 = Application.OpenForms["FrmListOrder"];
           if (f2 != null)
           {
               FrmListOrder frm3 = (FrmListOrder)Application.OpenForms["FrmListOrder"];
               Frm = frm3;
           }
           else
           {
               Frm = new FrmListOrder();
            }




           this.FLP = Flp;
       }

        public string _connectionString = ConnectionDB.ConstrReader;
       // System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
       private  SqlTableDependency<TblParent_FrooshKala> _dependency;
       public void WatchTable()
       {
           var mapper = new ModelToTableMapper<TblParent_FrooshKala>();
           mapper.AddMapping(model => model.ForooshKalaParent_ID, "ForooshKalaParent_ID");
           _dependency = new SqlTableDependency<TblParent_FrooshKala>(_connectionString, "TblParent_FrooshKala", "dbo", mapper);
           _dependency.OnChanged += _dependency_OnChanged;
           _dependency.OnError += _dependency_OnError;
           
       }

       public void StartTableWatcher()
       {
           _dependency.Start();
       }
       public void StopTableWatcher()
       {
           _dependency.Stop();
       }
       void _dependency_OnError(object sender, TableDependency.EventArgs.ErrorEventArgs e)
       {
          
           throw e.Error;
       }


       void _dependency_OnChanged(object sender, TableDependency.EventArgs.RecordChangedEventArgs<TblParent_FrooshKala> e)
       {
          
               if (e.ChangeType != ChangeType.None)
               {
                       var ChaneEntity = e.Entity;
                   switch (e.ChangeType)
                   {
                       case ChangeType.Delete:
                         
                    
                           if (Frm.InvokeRequired)
                           {
                              
                            Frm. DeleteCard(ChaneEntity.ForooshKalaParent_ShomareFish,ChaneEntity.ForooshKalaParent_Ready,ChaneEntity.ForooshKalaParent_Delivery,true);
                           }
                           else
                           {
                              Frm. DeleteCard(ChaneEntity.ForooshKalaParent_ShomareFish,ChaneEntity.ForooshKalaParent_Ready,ChaneEntity.ForooshKalaParent_Delivery,true);
                           }
                         

                           break;
                       case ChangeType.Insert:

                           Thread.Sleep(500);
                           DataTable dt = dtfood(ChaneEntity.ForooshKalaParent_ID.ToString());
                       

                           if (Frm.InvokeRequired)
                           {
                              
                               AddCard(ChaneEntity.ForooshKalaParent_ShomareFish, ChaneEntity.ForooshKalaParent_Tahvilgirande, ChaneEntity.ForooshKalaParent_ModateEntezar, ChaneEntity.ForooshKalaParent_TypeFact, ChaneEntity.ForooshKalaParent_Ready,dt);

                           }
                           else
                           {
                               AddCard(ChaneEntity.ForooshKalaParent_ShomareFish, ChaneEntity.ForooshKalaParent_Tahvilgirande, ChaneEntity.ForooshKalaParent_ModateEntezar, ChaneEntity.ForooshKalaParent_TypeFact, ChaneEntity.ForooshKalaParent_Ready,dt);

                           }
                         

                           break;

                       case ChangeType.Update:
                        
                           if (Frm.InvokeRequired)
                           {
                    
                              Frm. DeleteCard(ChaneEntity.ForooshKalaParent_ShomareFish,ChaneEntity.ForooshKalaParent_Ready,ChaneEntity.ForooshKalaParent_Delivery,false);
                           }
                           else
                           {
                               Frm.DeleteCard(ChaneEntity.ForooshKalaParent_ShomareFish,ChaneEntity.ForooshKalaParent_Ready,ChaneEntity.ForooshKalaParent_Delivery,false);

                           }
                         


                           break;
                   }
                 
               }



           }

       CardView.CardViewKitchen cvd = new CardView.CardViewKitchen();
       bool add = false;

              public void AddCard(string numfish, string namecustomer, int time,string TypeFact,bool Readyfish,DataTable dtlistfood)
                {

                    //try
                    //{


                       if (add)
                       {

                           cvd = new CardView.CardViewKitchen();
                           cvd.Minute = time;
                           cvd.TimerControl = true;
                           cvd.Name = numfish;
                           cvd.DataGridFill = dtlistfood;
                           cvd.CaptionStatusFish = TypeFact;
                           cvd.NumberFish = numfish;


                           if (Readyfish )
                           {
                               cvd.StopCard();
                           }


                       }
                
                        
             

                        if (Frm.FLP.InvokeRequired)
                        {
                            Frm.FLP.Invoke((MethodInvoker)delegate() {
                                add = true;
                                AddCard(numfish, namecustomer, time, TypeFact, Readyfish,dtlistfood);
                            });

                            //FLP.Invoke(new Action(() =>    FLP.Controls.Add(cvd);  ));
                             //cvd.RefreshCard();
                        }
                        else
                        {
                            Frm.FLP.Controls.Add(cvd);
                            add = false;
                        }

            
                    



                    //}
                    //catch (Exception ex)
                    //{
                    //    //func.WriteErrorLog(ex);
                    //    //NotificationManager.Show(Me, "خطا در لاگ ثبت گردید", Color.Red, 3000)
                    //}
                }
                public void DeleteCard(string numfish)
                {
                    //try
                    //{
                    foreach (CardView.CardViewKitchen item in FLP.Controls)
                        {
                            //if (!item.TimerControl)
                            //{
                            //    FLP.Controls.Remove(item);
                            //}

                            if (item.Name == numfish)
                            {
                                //FLP.Controls.Remove(item);
                                if (Frm.InvokeRequired)
                                {
                        FLP.BeginInvoke(new MethodInvoker(()=> FLP.Controls.Remove(item)  ));
                                }
                                else
                                {
                                    FLP.Controls.Remove(item);
                                }
                               

                            }
                        }

                    //}
                    //catch (Exception ex)
                    //{
                    //    //func.WriteErrorLog(ex);
                    //    //NotificationManager.Show(Me, "خطا در لاگ ثبت گردید", Color.Red, 3000)
                    //}
                }

                DataTable dtfood(string idfactor)
                {
                 DataTable dt = Func.GetFood(_connectionString, idfactor);

                    return dt;
                    
                }

    }
    
