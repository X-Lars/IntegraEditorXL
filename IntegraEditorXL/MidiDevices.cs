using IntegraXL.Core;
using IntegraXL.Interfaces;
using MidiXL;
using System;
using System.Collections.Generic;

namespace IntegraEditorXL
{
    public class MidiXLOutputDevice : IMIDIOutputDevice
    {
        private MidiOutputDevice _MidiOutputDevice;

        public MidiXLOutputDevice(int id)
        {
            _MidiOutputDevice = DeviceManager.GetMidiOutputDevice(id);

            ID = id;
            Name = _MidiOutputDevice.Name;
        }

        #region Interfaces: IMidiOutputDevice

        public int ID { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<MidiXLOutputDevice> GetDevices
        {
            get
            {
                List<MidiXLOutputDevice> devices = new();

                foreach (var device in DeviceManager.MidiOutputDevices)
                {
                    devices.Add(new MidiXLOutputDevice(device.ID));
                }

                return devices;
            }
        }

        public void Close()
        {
            _MidiOutputDevice.Close();
        }

        public void Open()
        {
            _MidiOutputDevice.Open();
        }

        public void SendLongMessage(byte[] data)
        {
            _MidiOutputDevice.Send(new SystemExclusiveMessage(data));
        }

        public void SendNoteOn(int channel, int note, int velocity)
        {
            _MidiOutputDevice.Send(new NoteOnMessage((MidiChannels)channel, note, velocity));
        }

        #endregion
    }

    public class MidiXLInputDevice : IMIDIInputDevice
    {
        private MidiInputDevice _MidiInputDevice;

        public MidiXLInputDevice(int id)
        {
            _MidiInputDevice = DeviceManager.GetMidiInputDevice(id);
            _MidiInputDevice.SystemExclusiveReceived += SystemExclusiveReceived;
            _MidiInputDevice.UniversalNonRealTimeReceived += UniversalNonRealTimeReceived;

            ID = id;
            Name = _MidiInputDevice.Name;
        }

        private void UniversalNonRealTimeReceived(object sender, UniversalNonRealTimeMessageEventArgs e)
        {
            LongMessageEventHandler(sender, new LongMessageEventArgs(e.Message.Data));
        }

        private void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            LongMessageEventHandler(sender, new LongMessageEventArgs(e.Message.Data));
        }



        #region Interfaces: IMidiInputDevice

        public event EventHandler<LongMessageEventArgs> LongMessageReceived;

        public int ID { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<MidiXLInputDevice> GetDevices
        {
            get
            {
                List<MidiXLInputDevice> devices = new ();

                foreach (var device in DeviceManager.MidiInputDevices)
                    devices.Add(new MidiXLInputDevice(device.ID) { Name = device.Name });

                return devices;
            }
        }

        public void Close()
        {
            _MidiInputDevice.Close();
        }

        public void Open()
        {
            _MidiInputDevice.Open();
        }

        public void Start()
        {
            _MidiInputDevice.Start();
        }

        public void LongMessageEventHandler(object sender, LongMessageEventArgs e)
        {
            LongMessageReceived?.Invoke(this, e);
        }

        #endregion
    }
}
