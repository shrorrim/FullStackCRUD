using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.WpfClient
{
    public class YearInfo
    {

        public int Year { get; set; }
        public int Month { get; set; }
        public int IncomeByMonth { get; set; }

        public override string ToString()
        {
            return $"Year: {Year}, Month: {Month}, Income: {IncomeByMonth}";
        }
    }
    public class MainWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        private ObservableCollection< KeyValuePair<string,double> > avgPriceByBrands;
        public ObservableCollection<KeyValuePair<string, double>> AvgPriceByBrands
        {
            get { return avgPriceByBrands; }
            set
            {
                SetProperty(ref avgPriceByBrands, value);
            }
        }

        private ObservableCollection<Orderer> mostPayingOrderers;
        public ObservableCollection<Orderer> MostPayingOrderers
        {
            get { return mostPayingOrderers; }
            set
            {
                SetProperty(ref mostPayingOrderers, value);
            }
        }

        private ObservableCollection<Brand> mostPopularBrands;
        public ObservableCollection<Brand> MostPopularBrands
        {
            get { return mostPopularBrands; }
            set
            {
                SetProperty(ref mostPopularBrands, value);
            }
        }

        private ObservableCollection<Laptop> mostPopularLaptopModels;
        public ObservableCollection<Laptop> MostPopularLaptopModels
        {
            get { return mostPopularLaptopModels; }
            set
            {
                SetProperty(ref mostPopularLaptopModels, value);
            }
        }

        private ObservableCollection<YearInfo> getStatByYear;
        public ObservableCollection<YearInfo> GetStatByYear
        {
            get { return getStatByYear; }
            set
            {
                SetProperty(ref getStatByYear, value);
            }
        }


        private RestCollection<Laptop> laptops;
        public RestCollection<Laptop> Laptops
        {
            get { return laptops; }
            set
            {
                SetProperty(ref laptops, value);
            }
        }

        private RestCollection<Brand> brands;
        public RestCollection<Brand> Brands
        {
            get { return brands; }
            set
            {
                SetProperty(ref brands, value);
            }
        }

        private RestCollection<Orderer> orderers;
        public RestCollection<Orderer> Orderers
        {
            get { return orderers; }
            set
            {
                SetProperty(ref orderers, value);
            }
        }

        private RestCollection<Order> orders;
        public RestCollection<Order> Orders
        {
            get { return orders; }
            set
            {
                SetProperty(ref orders, value);
            }
        }

        private ObservableCollection<object> selectedCollection;
        public ObservableCollection<object> SelectedCollection
        {
            get { return selectedCollection; }
            set
            {
                SetProperty(ref selectedCollection, value);
            }
        }

        private string selectedOption;
        public string SelectedOption
        {
            get { return selectedOption; }
            set
            {
                SetProperty(ref selectedOption, value);
                (WorkWithLaptops as RelayCommand).NotifyCanExecuteChanged();
                (WorkWithBrands as RelayCommand).NotifyCanExecuteChanged();
                (WorkWithOrders as RelayCommand).NotifyCanExecuteChanged();
                (WorkWithOrderers as RelayCommand).NotifyCanExecuteChanged();
                (StatAvgPriceByBrands as RelayCommand).NotifyCanExecuteChanged();
                (StatGetStatByYear as RelayCommand).NotifyCanExecuteChanged();
                (StatMostPayingOrderers as RelayCommand).NotifyCanExecuteChanged();
                (StatMostPopularBrands as RelayCommand).NotifyCanExecuteChanged();
                (StatMostPopularLaptopModels as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand WorkWithLaptops { get; set; }
        public ICommand WorkWithBrands { get; set; }
        public ICommand WorkWithOrders { get; set; }
        public ICommand WorkWithOrderers { get; set; }


        //CRUD
        public ICommand Create { get; set; }
        public ICommand Read { get; set; }
        public ICommand Update { get; set; }
        public ICommand Delete { get; set; }


        //nonCruds

        public ICommand StatAvgPriceByBrands { get; set; }
        public ICommand StatMostPayingOrderers { get; set; }
        public ICommand StatMostPopularBrands { get; set; }
        public ICommand StatMostPopularLaptopModels { get; set; }
        public ICommand StatGetStatByYear { get; set; }

        enum States {Initial,InNonCruds,InLaptops,InBrands,InOrders,InOrderers } //what state the program is in


        //common selected item:

        private object selectedElement;

        public object SelectedElement
        {
            get { return selectedElement; }
            set
            {
                if (value != null)
                {
                    if(value is Laptop)
                    {
                        Laptop copy = new Laptop();
                        foreach (var property in typeof(Laptop).GetProperties())
                        {
                            var valueToCopy = property.GetValue((value as Laptop));
                            property.SetValue(copy, valueToCopy);
                        }

                        copy.Brand = (value as Laptop).Brand;

                        selectedElement = copy;
                    }
                    else if(value is Brand)
                    {
                        Brand copy = new Brand();
                        foreach (var property in typeof(Brand).GetProperties())
                        {
                            var valueToCopy = property.GetValue((value as Brand));
                            property.SetValue(copy, valueToCopy);
                        }

                        selectedElement = copy;
                    }
                    else if (value is Orderer)
                    {
                        Orderer copy = new Orderer();
                        foreach (var property in typeof(Orderer).GetProperties())
                        {
                            var valueToCopy = property.GetValue((value as Orderer));
                            property.SetValue(copy, valueToCopy);
                        }

                        selectedElement = copy;
                    }
                    else if (value is Order)
                    {
                        Order copy = new Order();
                        foreach (var property in typeof(Order).GetProperties())
                        {
                            var valueToCopy = property.GetValue((value as Order));
                            property.SetValue(copy, valueToCopy);
                        }

                        selectedElement = copy;
                    }

                    OnPropertyChanged(nameof(SelectedElement));
                    (Delete as RelayCommand).NotifyCanExecuteChanged();
                    (Update as RelayCommand).NotifyCanExecuteChanged();
                }

            }
        }

        private States state;
      

        private void RestCollectionsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(state != States.Initial)
            {
                //NonCrudMethodResults:
                var jsonAvgPriceByBrands = new WebClient().DownloadString("http://localhost:27376/stat/avgpricebybrands");
                AvgPriceByBrands = JsonConvert.DeserializeObject<ObservableCollection<KeyValuePair<string, double>>>(jsonAvgPriceByBrands);

                var jsonMostPayingOrderers = new WebClient().DownloadString("http://localhost:27376/stat/mostpayingorderers");
                MostPayingOrderers = JsonConvert.DeserializeObject<ObservableCollection<Orderer>>(jsonMostPayingOrderers);

                var jsonMostPopularBrands = new WebClient().DownloadString("http://localhost:27376/stat/mostpopularbrands");
                MostPopularBrands = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(jsonMostPopularBrands);


                var jsonMostPopularLaptopModels = new WebClient().DownloadString("http://localhost:27376/stat/MostPopularLaptopModels");
                MostPopularLaptopModels = JsonConvert.DeserializeObject<ObservableCollection<Laptop>>(jsonMostPopularLaptopModels);

                var jsonForYearInfo2023 = new WebClient().DownloadString("http://localhost:27376/stat/getstatbyyear/2023");
                GetStatByYear = JsonConvert.DeserializeObject<ObservableCollection<YearInfo>>(jsonForYearInfo2023);
            }

            UpdateSelectedCollection();
        }

        public MainWindowViewModel()
        {
            state = States.Initial;

            if (!IsInDesignMode)
            {
                
                SelectedCollection = new ObservableCollection<object>(); // commonstore

                //RestCollections:
                Laptops = new RestCollection<Laptop>("http://localhost:27376/", "laptop","hub");
                Brands = new RestCollection<Brand>("http://localhost:27376/", "brand", "hub");
                Orders = new RestCollection<Order>("http://localhost:27376/", "order", "hub");
                Orderers = new RestCollection<Orderer>("http://localhost:27376/", "orderer", "hub");

                //NonCrudMethodResults:
                var jsonAvgPriceByBrands = new WebClient().DownloadString("http://localhost:27376/stat/avgpricebybrands");
                AvgPriceByBrands = JsonConvert.DeserializeObject<ObservableCollection<KeyValuePair<string, double>>>(jsonAvgPriceByBrands);

                var jsonMostPayingOrderers = new WebClient().DownloadString("http://localhost:27376/stat/mostpayingorderers");
                MostPayingOrderers = JsonConvert.DeserializeObject<ObservableCollection<Orderer>>(jsonMostPayingOrderers);

                var jsonMostPopularBrands = new WebClient().DownloadString("http://localhost:27376/stat/mostpopularbrands");
                MostPopularBrands = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(jsonMostPopularBrands);


                var jsonMostPopularLaptopModels = new WebClient().DownloadString("http://localhost:27376/stat/MostPopularLaptopModels");
                MostPopularLaptopModels = JsonConvert.DeserializeObject<ObservableCollection<Laptop>>(jsonMostPopularLaptopModels);

                var jsonForYearInfo2023 = new WebClient().DownloadString("http://localhost:27376/stat/getstatbyyear/2023");
                GetStatByYear = JsonConvert.DeserializeObject<ObservableCollection<YearInfo>>(jsonForYearInfo2023);


                Laptops.CollectionChanged += RestCollectionsChanged;
                Brands.CollectionChanged += RestCollectionsChanged;
                Orders.CollectionChanged += RestCollectionsChanged;
                Orderers.CollectionChanged += RestCollectionsChanged;

                WorkWithLaptops = new RelayCommand(() =>
                {
                    SelectedOption = "Laptops";
                    state = States.InLaptops;
                    Laptops.Update(Laptops.Last());
                    UpdateSelectedCollection();
                });

                WorkWithBrands = new RelayCommand(() =>
                {
                    state = States.InBrands;
                    SelectedOption = "Brands";
                    Brands.Update(Brands.Last());
                    UpdateSelectedCollection();
                });

                WorkWithOrders = new RelayCommand(() =>
                {
                    state = States.InOrders;
                    SelectedOption = "Orders";
                    Orders.Update(Orders.Last());
                    UpdateSelectedCollection();
                });

                WorkWithOrderers = new RelayCommand(() =>
                {
                    state = States.InOrderers;
                    SelectedOption = "Orderers";
                    Orderers.Update(Orderers.Last());
                    UpdateSelectedCollection();
                });


                Delete = new RelayCommand(() =>
                {

                    if (selectedElement is Laptop )
                    {
                        Laptops.Delete((selectedElement as Laptop).Id);
                    }
                    else if(selectedElement is Brand)
                    {
                        Brands.Delete((selectedElement as Brand).Id);
                    }
                    else if (selectedElement is Order)
                    {
                        Orders.Delete((selectedElement as Order).Id);
                    }
                    else if (selectedElement is Orderer)
                    {
                        Orderers.Delete((selectedElement as Orderer).Id);
                    }

                }, () => { return state != States.InNonCruds; });

                Create = new RelayCommand(() =>
                {
                    if(state == States.InLaptops)
                    {
                        
                        SelectedOption = "Laptops";

                        Laptop temp = new Laptop()
                        {
                            Brand = ((SelectedElement as Laptop).Brand as Brand),
                            ModelName = (SelectedElement as Laptop).ModelName,
                            Processor = (SelectedElement as Laptop).Processor,
                            RAM = (SelectedElement as Laptop).RAM,
                            RAM_Upgradeable = (SelectedElement as Laptop).RAM_Upgradeable,
                            Price = (SelectedElement as Laptop).Price,
                            Storage = (SelectedElement as Laptop).Storage
                        };

                        Laptops.Add(temp);

                        Laptops.Update(Laptops.Last());
                        
                    }
                    else if(state == States.InBrands)
                    {
                        SelectedOption = "Brands";
                        Brands.Add(new Brand()
                        {
                            Name = (SelectedElement as Brand).Name,
                            YearOfAppearance = (SelectedElement as Brand).YearOfAppearance
                        });

                        Brands.Update(Brands.Last());
                    }
                    else if (state == States.InOrders)
                    {
                        SelectedOption = "Orders";
                        Orders.Add(new Order()
                        {
                            OrdererId = (SelectedElement as Order).OrdererId,
                            LaptopId = (SelectedElement as Order).LaptopId,
                            Date = (SelectedElement as Order).Date
                        });

                        Orders.Update(Orders.Last());
                    }
                    else if (state == States.InOrderers)
                    {
                        SelectedOption = "Orderers";
                        Orderers.Add(new Orderer()
                        {
                            Name = (SelectedElement as Orderer).Name,
                            PhoneNumber = (SelectedElement as Orderer).PhoneNumber
                        });
                        Orderers.Update(Orderers.Last());
                    }

                });

                Update = new RelayCommand(() =>
                {
                    
                    if (SelectedElement is Laptop)
                    {
                        SelectedOption = "Laptops";
                        Laptops.Update((SelectedElement as Laptop));
                        Laptops.Update(Laptops.Last());
                    }
                    else if (SelectedElement is Brand)
                    {
                        SelectedOption = "Brands";
                        Brands.Update((SelectedElement as Brand));
                        Brands.Update(Brands.Last());
                    }
                    else if (SelectedElement is Order)
                    {
                        SelectedOption = "Orders";
                        Orders.Update((SelectedElement as Order));
                        Orders.Update(Orders.Last());
                    }
                    else if (SelectedElement is Orderer)
                    {
                        SelectedOption = "Orderers";
                        Orderers.Update((SelectedElement as Orderer));
                        Orderers.Update(Orderers.Last());   
                    }

                });



                StatAvgPriceByBrands = new RelayCommand(() =>
                {
                    state = States.InNonCruds;

                    //refresh if changed
                    var jsonAvgPriceByBrands = new WebClient().DownloadString("http://localhost:27376/stat/avgpricebybrands");
                    AvgPriceByBrands = JsonConvert.DeserializeObject<ObservableCollection<KeyValuePair<string, double>>>(jsonAvgPriceByBrands);

                    SelectedOption = "AvgPriceByBrands";
                    UpdateSelectedCollection();
                });

                StatMostPayingOrderers = new RelayCommand(() =>
                {
                    state = States.InNonCruds;

                    var jsonMostPayingOrderers = new WebClient().DownloadString("http://localhost:27376/stat/mostpayingorderers");
                    MostPayingOrderers = JsonConvert.DeserializeObject<ObservableCollection<Orderer>>(jsonMostPayingOrderers);

                    SelectedOption = "MostPayingOrderers";
                    UpdateSelectedCollection();
                });

                StatMostPopularBrands = new RelayCommand(() =>
                {
                    state = States.InNonCruds;

                    var jsonMostPopularBrands = new WebClient().DownloadString("http://localhost:27376/stat/mostpopularbrands");
                    MostPopularBrands = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(jsonMostPopularBrands);

                    SelectedOption = "MostPopularBrands";
                    UpdateSelectedCollection();
                });


                StatMostPopularLaptopModels = new RelayCommand(() =>
                {
                    state = States.InNonCruds;

                    var jsonMostPopularLaptopModels = new WebClient().DownloadString("http://localhost:27376/stat/MostPopularLaptopModels");
                    MostPopularLaptopModels = JsonConvert.DeserializeObject<ObservableCollection<Laptop>>(jsonMostPopularLaptopModels);

                    SelectedOption = "MostPopularLaptopModels";
                    UpdateSelectedCollection();
                });

                StatGetStatByYear = new RelayCommand(() =>
                {
                    state = States.InNonCruds;

                    var jsonForYearInfo2023 = new WebClient().DownloadString("http://localhost:27376/stat/getstatbyyear/2023");
                    GetStatByYear = JsonConvert.DeserializeObject<ObservableCollection<YearInfo>>(jsonForYearInfo2023);

                    SelectedOption = "GetStatByYear";
                    UpdateSelectedCollection();
                });

            }  
        }

        private void UpdateSelectedCollection()
        {

            SelectedCollection.Clear();

            switch (SelectedOption)
            {
                case "Laptops":
                    foreach (var laptop in Laptops)
                    {
                        SelectedCollection.Add(laptop);
                    }
                    

                    break;
                case "Brands":
                    foreach (var brand in Brands)
                    {
                        SelectedCollection.Add(brand);
                    }

                    break;
                case "Orderers":

                    foreach (var orderer in Orderers)
                    {
                        SelectedCollection.Add(orderer);
                    }
                    break;
                case "Orders":
                    foreach (var order in Orders)
                    {
                        SelectedCollection.Add(order);
                    }

                    break;

                case "AvgPriceByBrands":

                    foreach (var element in AvgPriceByBrands)
                    {
                        SelectedCollection.Add(element);
                    }
                    break;

                case "MostPayingOrderers":

                    foreach (var element in MostPayingOrderers)
                    {
                        SelectedCollection.Add(element);
                    }
                    break;

                case "MostPopularBrands":

                    foreach (var element in MostPopularBrands)
                    {
                        SelectedCollection.Add(element);
                    }
                    break;

                case "MostPopularLaptopModels":

                    foreach (var element in MostPopularLaptopModels)
                    {
                        SelectedCollection.Add(element);
                    }
                    break;

                case "GetStatByYear":

                    foreach (var element in GetStatByYear)
                    {
                        SelectedCollection.Add(element);
                    }
                    break;

                default:
                    break;
            }


            SelectedElement = SelectedCollection.First();
        }





    }
}

