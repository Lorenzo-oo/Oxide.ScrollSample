using Network;
using Oxide.Core.Libraries.Covalence;
using Oxide.Game.Rust.Cui;
using UnityEngine;
using UnityEngine.UI;
namespace Oxide.Plugins;

[Info("Scroll Sample", "Lorenzo", "1.0.0")]
[Description("Sample code for scroll CUI")]
class ScrollSample : CovalencePlugin
{
    #region hooks

    private void OnClientCommand(Connection connection, string text)
    {
        Puts($"OnClientCommand {text}");
    }

    #endregion hooks


    [Command("scrollui")]
    private void draggableUI(IPlayer iplayer, string command, string[] args)
    {
        Puts("show scroll UI");

        var player = iplayer.Object as BasePlayer;
        if (player != null)
        {
            CuiElementContainer ScrollSample = createScrollUI();
            CuiHelper.AddUi(player, ScrollSample);
            return;
        }
    }

    [Command("hidescrollui")]
    private void deldraggableUI(IPlayer iplayer, string command, string[] args)
    {
        Puts("Hide scroll UI");

        var player = iplayer.Object as BasePlayer;
        if (player != null)
        {
            CuiHelper.DestroyUi(player, UIScrollName);
            return;
        }
    }

    [Command("ui.testscrollui")]
    private void testscrollui(IPlayer iplayer, string command, string[] args)
    {
        int num = int.Parse(args[0]);
        Puts($"Button {num} pressed in scroll UI   {args[0]}");


    }

    public const string UIScrollName = "UI.Scrolltest";

    CuiElementContainer createScrollUI()
    {
        CuiElementContainer elements = new CuiElementContainer();

        string mainpanel = elements.Add(new CuiPanel
        {
            Image = { Color = "0.3 0.3 0.6 0.8" },
            RectTransform = { AnchorMin = "0.5 0.5", AnchorMax = "0.5 0.5", OffsetMin = "-200 -300", OffsetMax = "200 300" },
        }, "Overlay", UIScrollName, UIScrollName);

        elements.Add(new CuiElement
        {
            Name = "panel1",
            Parent = UIScrollName,
            Components =
                {
                    new CuiRawImageComponent { Sprite = "Assets/Content/UI/UI.Background.Tile.psd", Color = "0.1 0.1 0.1 1.0" },
                    new CuiRectTransformComponent { AnchorMin = "0.1 0.05", AnchorMax = "0.9 0.95" }
                }
        });

        elements.Add(new CuiElement
        {
            Parent = "panel1",
            Components =
                {
                    new CuiTextComponent { Text = "Scroll example", FontSize=12, Align=TextAnchor.MiddleCenter },
                    new CuiRectTransformComponent { AnchorMin = "0 1", AnchorMax = "1 1", OffsetMin = "0 -50", OffsetMax = "0 0" }                    
                }
        });

        elements.Add(new CuiButton
        {
            Button = { Color = "0.8 0.2 0.2 0.8", Command = "hidescrollui" },
            RectTransform = { AnchorMin = "0.92 0.93", AnchorMax = "0.99 0.98" },
            Text = { Text = "Close", FontSize = 8, Align = TextAnchor.MiddleCenter }
        }, mainpanel);

        CuiScrollViewComponent scrollUI = new CuiScrollViewComponent
        {
            ContentTransform = new CuiRectTransform { AnchorMin = "0 0.9", AnchorMax = "1 0.9", OffsetMin = "0 -1200", OffsetMax = "0 0" },
            Vertical = true,
            Horizontal = false,
            MovementType = ScrollRect.MovementType.Clamped,
            Elasticity = 0.25f,
            Inertia = true,
            DecelerationRate = 0.3f,
            ScrollSensitivity = 24f,
            VerticalScrollbar = new CuiScrollbar { AutoHide = true, Size = 20 },
            HorizontalScrollbar = new CuiScrollbar { AutoHide = true, Size = 20 },
        };

        elements.Add(new CuiElement
        {
            Name = "Scroller",
            Parent = "panel1",
            Components =
                {
                new CuiRawImageComponent { Sprite = "assets/content/effects/crossbreed/fx gradient skewed.png", Color = "0.15 0.25 0.25 0.8" },
                scrollUI,
                new CuiRectTransformComponent {AnchorMin = "0 0", AnchorMax = "1 1", OffsetMax = "0 -50" },
                new CuiNeedsCursorComponent()
                }
            });

        int scrollSpacing = 28;
        int scrollSize = 26;

       
        for ( int i=0; i<40; i++) 
        {
            Puts($"OffsetMin =  {-i * scrollSpacing} /  OffsetMax =  {-i * scrollSpacing - scrollSize}");
            elements.Add(new CuiElement
            {
                Name = "Text_" + i,
                Parent = "Scroller",
                Components =
                {
                new CuiRawImageComponent { Color = "0.2 0.2 0.2 1.0", Sprite = "Assets/Content/UI/UI.Background.Tile.psd" },
                new CuiRectTransformComponent { AnchorMin = "0.03 0.97", AnchorMax = "0.75 0.97", OffsetMin = $"0 {-i*scrollSpacing-scrollSize}", OffsetMax = $"0 {-i*scrollSpacing}"},
                new CuiOutlineComponent { Distance= "2 2", Color= "0 0 0 0.06" },
                new CuiOutlineComponent { Distance= "4 4", Color= "0 0 0 0.025" },
                }
            });

            elements.Add(new CuiElement
            {
                Parent = "Text_" + i,
                Components =
                {
                new CuiTextComponent { Text = $"Item #{i+1}", Color = "0.95 0.95 0.95 1.0", FontSize = 10, Align = TextAnchor.MiddleLeft },          
                }
            });

            elements.Add(new CuiElement
            {
                Name = "Button_" + i,
                Parent = "Scroller",
                Components =
                {
                new CuiRawImageComponent { Color = "0.2 0.2 0.2 1.0", Sprite = "Assets/Content/UI/UI.Background.Tile.psd" },
                new CuiRectTransformComponent { AnchorMin = "0.77 0.97", AnchorMax = "0.97 0.97", OffsetMin = $"0 {-i*scrollSpacing-scrollSize}", OffsetMax = $"0 {-i*scrollSpacing}"},
                new CuiOutlineComponent { Distance= "2 2", Color= "0 0 0 0.06" },
                new CuiOutlineComponent { Distance= "4 4", Color= "0 0 0 0.025" },
                }
            });

            elements.Add(new CuiElement
            {
                Name = "ButtonText_" + i,
                Parent = "Button_" + i,
                Components =
                {
                new CuiTextComponent { Text = $"Button#{i+1}", Color = "0.95 0.95 0.95 1.0", FontSize = 8, Align = TextAnchor.MiddleCenter },
                new CuiRectTransformComponent { AnchorMin = "0.02 0.02", AnchorMax = "0.98 0.98"},
                }
            });

            elements.Add(new CuiElement
            {
                Name = "TheButton_" + i,
                Parent = "ButtonText_" + i,
                Components =
                {
                    new CuiButtonComponent { Color = "0.8 0.2 0.2 0.8", Command = "ui.testscrollui "+ (i+1), ImageType= Image.Type.Tiled },
                    new CuiRectTransformComponent { AnchorMin = "0.02 0.02", AnchorMax = "0.98 0.98" }
                }
            });
            /*
            elements.Add(new CuiElement
            {
                Parent = "TheButton_" + i,
                Components =
                {
                    new CuiTextComponent { Text = $"X #{i+1}", Color = "0.95 0.95 0.95 1.0", FontSize = 10, Align = TextAnchor.MiddleCenter },
                }
            });
            */
        }
        return elements;
    }

}