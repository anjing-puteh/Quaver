using System;
using System.Collections.Generic;
using System.Drawing;
using Quaver.Assets;
using Quaver.Graphics.Notifications;
using Quaver.Online;
using Quaver.Server.Client.Structures;
using Wobble.Graphics;
using Wobble.Graphics.BitmapFonts;
using Wobble.Graphics.Sprites;
using Wobble.Graphics.Transformations;
using Wobble.Window;
using Color = Microsoft.Xna.Framework.Color;

namespace Quaver.Graphics.Overlays.Chat.Components.Messages.Drawable
{
    public class DrawableChatMessage : Sprite
    {
        /// <summary>
        ///     The parent chat message container.
        /// </summary>
        public ChatMessageContainer Container { get; }

        /// <summary>
        ///     The user's avatar.
        /// </summary>
        public Sprite Avatar { get; }

        /// <summary>
        ///     The actual chat message.
        /// </summary>
        public ChatMessage Message { get; }

        /// <summary>
        ///     The username of the person that wrote the message.
        /// </summary>
        public SpriteTextBitmap TextUsername { get; private set; }

        /// <summary>
        ///     The actual content of the message.
        /// </summary>
        public SpriteTextBitmap TextMessageContent { get; private set; }

        /// <summary>
        ///     The amount of y space between the content and time sent.
        /// </summary>
        private int Padding { get; } = 10;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="container"></param>
        /// <param name="message"></param>
        public DrawableChatMessage(ChatMessageContainer container, ChatMessage message)
        {
            Container = container;
            Message = message;

            Avatar = new Sprite
            {
                Parent = this,
                X = 10,
                Size = new ScalableVector2(45, 45),
                Image = UserInterface.YouAvatar,
                Y = Padding,
            };

            X = -Container.Width;
            Width = Container.Width - 5;
            Alpha = 0;

            CreateUsernameText();
            CreateMessageContentText();
            RecalculateHeight();
        }

        /// <summary>
        ///    Creates the text of the user who wrote the message.
        /// </summary>
        private void CreateUsernameText()
        {
            var timespan = TimeSpan.FromMilliseconds(Message.Time);
            var date = (new DateTime(1970, 1, 1) + timespan).ToLocalTime();

            TextUsername = new SpriteTextBitmap(BitmapFonts.Exo2SemiBold, $"[{date.ToShortTimeString()}] {Message.Sender.Username}",
                14, Colors.GetUserChatColor(Message.Sender), Alignment.MidLeft, (int) WindowManager.Width)
            {
                Parent = this,
                X = Avatar.Width + Avatar.X + 5,
                Y = Avatar.Y - 3,
            };
        }

        /// <summary>
        ///    Creates the text that holds the message content.
        /// </summary>
        private void CreateMessageContentText() => TextMessageContent = new SpriteTextBitmap(BitmapFonts.Exo2Medium, Message.Message, 14,
            Color.White, Alignment.MidLeft, (int)(Container.Width - Avatar.Width - Avatar.X - 5))
        {
            Parent = this,
            X = TextUsername.X,
            Y = TextUsername.Y + TextUsername.Height - 1,
        };

        /// <summary>
        ///     Calculates the height of the message.
        /// </summary>
        private void RecalculateHeight()
        {
            Height = 0;
            var maxHeight = Math.Max(Avatar.Height, TextMessageContent.Height);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (maxHeight == Avatar.Height)
                Height = Avatar.Height;
            else
                Height = TextUsername.Height + maxHeight;

            Height += Padding * 2;
        }
    }
}