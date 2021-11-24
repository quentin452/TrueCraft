﻿using System;
using TrueCraft.Client.Windows;
using TrueCraft.Core;
using TrueCraft.Core.Logic;
using TrueCraft.Core.Networking;
using TrueCraft.Core.Networking.Packets;
using TrueCraft.Core.Windows;

namespace TrueCraft.Client.Handlers
{
    internal static class InventoryHandlers
    {
        public static void HandleWindowItems(IPacket _packet, MultiplayerClient client)
        {
            var packet = (WindowItemsPacket)_packet;
            if (packet.WindowID == 0)
                client.InventoryWindowContent.SetSlots(packet.Items);
            else
                client.CurrentWindow.SetSlots(packet.Items);
        }

        public static void HandleSetSlot(IPacket _packet, MultiplayerClient client)
        {
            var packet = (SetSlotPacket)_packet;
            IWindowContent window = null;
            if (packet.WindowID == 0)
                window = client.InventoryWindowContent;
            else
                window = client.CurrentWindow;
            if (window != null)
            {
                if (packet.SlotIndex >= 0 && packet.SlotIndex < window.Length)
                {
                    window[packet.SlotIndex] = new ItemStack(packet.ItemID, packet.Count, packet.Metadata);
                }
            }
        }

        public static void HandleOpenWindowPacket(IPacket _packet, MultiplayerClient client)
        {
            var packet = (OpenWindowPacket)_packet;
            IWindowContentClient window = null;
            switch (packet.Type)
            {
                case WindowType.CraftingBench:
                    window = new CraftingBenchWindowContentClient(client.Inventory, client.Hotbar,
                        CraftingRepository.Get(), BlockProvider.ItemRepository);
                    break;

                case WindowType.Chest:
                    window = new ChestWindowContentClient(client.Inventory, client.Hotbar,
                        packet.TotalSlots == 2 * ChestWindowConstants.ChestLength,
                        BlockProvider.ItemRepository);
                    break;
            }

            // TODO: For any window type other than CraftingBench or Chest, window will be null.
            window.ID = packet.WindowID;
            client.CurrentWindow = window;
        }

        public static void HandleCloseWindowPacket(IPacket _packet, MultiplayerClient client)
        {
            client.CurrentWindow = null;
        }

        public static void HandleTransactionStatusPacket(IPacket packet, MultiplayerClient client)
        {
            TransactionStatusPacket statusPacket = (TransactionStatusPacket)packet;
            ActionConfirmation action = ActionList.Get(statusPacket.ActionNumber);

            if (object.ReferenceEquals(action, null))
                throw new ApplicationException($"Unexpected Action Number from server: {statusPacket.ActionNumber}");

            action.TakeAction();
        }
    }
}