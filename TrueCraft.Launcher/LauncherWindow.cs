﻿using System;
using System.Diagnostics;
using System.Reflection;
using Gtk;
using TrueCraft.Launcher.Views;
using TrueCraft.Core;

namespace TrueCraft.Launcher
{
    public class LauncherWindow : Window
    {
        public TrueCraftUser User { get; set; }

        public HBox MainContainer { get; set; }
        public ScrolledWindow WebScrollView { get; set; }

        // TODO Change from Label to a Web Browser
        public Label WebView { get; set; }

        public LoginView LoginView { get; set; }
        public MainMenuView MainMenuView { get; set; }
        public OptionView OptionView { get; set; }
        public MultiplayerView MultiplayerView { get; set; }
        public SingleplayerView SingleplayerView { get; set; }
        public VBox InteractionBox { get; set; }
        public Image TrueCraftLogoImage { get; set; }

        public LauncherWindow()
        {
            this.Title = "TrueCraft Launcher";
            this.DefaultSize = new Gdk.Size(1200, 576);
            this.User = new TrueCraftUser();

            MainContainer = new HBox();
            WebScrollView = new ScrolledWindow();
            WebView = new Label("https://truecraft.io/updates");
            LoginView = new LoginView(this);
            OptionView = new OptionView(this);
            MultiplayerView = new MultiplayerView(this);
            SingleplayerView = new SingleplayerView(this);
            InteractionBox = new VBox();
            
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TrueCraft.Launcher.Content.truecraft_logo.png"))
                TrueCraftLogoImage = new Image(new Gdk.Pixbuf(stream, 350, 75));

            WebScrollView.Content = WebView;
            MainContainer.PackStart(WebScrollView, true, false, 0);
            InteractionBox.PackStart(TrueCraftLogoImage, true, false, 0);
            InteractionBox.PackEnd(LoginView, true, false, 0);
            MainContainer.PackEnd(InteractionBox, true, false, 0);

            this.Content = MainContainer;
        }

        void ClientExited()
        {
            this.Show();
            this.ShowInTaskbar = true;
        }
    }
}
