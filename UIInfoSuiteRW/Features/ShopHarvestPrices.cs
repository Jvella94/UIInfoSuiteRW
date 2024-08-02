﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using UIInfoSuiteRW.Utils;

namespace UIInfoSuiteRW.Features
{
  internal class ShopHarvestPrices : IFeature
  {
    #region Properties
    private readonly IModHelper _helper;
    #endregion

    #region Lifecycle
    public ShopHarvestPrices(IModHelper helper)
    {
      _helper = helper;
    }
    
    public void ToggleOption(bool toggle)
    {
      _helper.Events.Display.RenderedActiveMenu -= OnRenderedActiveMenu;

      if (toggle)
      {
        _helper.Events.Display.RenderedActiveMenu += OnRenderedActiveMenu;
      }
    }
    #endregion

    #region Event subscrriptions
    private void OnRenderedActiveMenu(object? sender, RenderedActiveMenuEventArgs e)
    {
      if (Game1.activeClickableMenu is not ShopMenu menu)
      {
        return;
      }

      if (menu.hoveredItem is not Item hoverItem)
      {
        return;
      }

      // draw shop harvest prices
      int value = Tools.GetHarvestPrice(hoverItem);

      if (value > 0)
      {
        int xPosition = menu.xPositionOnScreen - 30;
        int yPosition = menu.yPositionOnScreen + 580;
        IClickableMenu.drawTextureBox(Game1.spriteBatch, xPosition + 20, yPosition - 52, 264, 108, Color.White);
        
        // Title "Harvest Price"
        string textToRender = I18n.HarvestPrice();
        
        Game1.spriteBatch.DrawString(
          Game1.dialogueFont,
          textToRender,
          new Vector2(xPosition + 30, yPosition - 38),
          Color.Black * 0.2f
        );
        Game1.spriteBatch.DrawString(
          Game1.dialogueFont,
          textToRender,
          new Vector2(xPosition + 32, yPosition - 40),
          Color.Black * 0.8f
        );
        // Tree Icon
        xPosition += 80;
        Game1.spriteBatch.Draw(
          Game1.mouseCursors,
          new Vector2(xPosition, yPosition),
          new Rectangle(60, 428, 10, 10),
          Color.White,
          0,
          Vector2.Zero,
          Game1.pixelZoom,
          SpriteEffects.None,
          0.85f
        );
        //  Coin
        Game1.spriteBatch.Draw(
          Game1.debrisSpriteSheet,
          new Vector2(xPosition + 32, yPosition + 10),
          Game1.getSourceRectForStandardTileSheet(Game1.debrisSpriteSheet, 8, 16, 16),
          Color.White,
          0,
          new Vector2(8, 8),
          4,
          SpriteEffects.None,
          0.95f
        );
        // Price
        Game1.spriteBatch.DrawString(
          Game1.dialogueFont,
          value.ToString(),
          new Vector2(xPosition + 50, yPosition + 6),
          Color.Black * 0.2f
        );
        Game1.spriteBatch.DrawString(
          Game1.dialogueFont,
          value.ToString(),
          new Vector2(xPosition + 52, yPosition + 4),
          Color.Black * 0.8f
        );
      }
    }
  }
  #endregion
}
