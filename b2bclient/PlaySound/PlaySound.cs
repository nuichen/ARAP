using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.PlaySound
{
    class PlaySound:IPlaySound 
    {
        private DateTime PlaySoundForNewOrder_time = DateTime.MinValue;
        private DateTime PlaySoundForNewCus_time = DateTime.MinValue;
        void IPlaySound.PlaySoundForNewOrder()
        {
            if (Program.lstNewOrder.Count ==0)
            {
                this.PlaySoundForNewOrder_time = DateTime.MinValue;
            }
            else if (this.PlaySoundForNewOrder_time.AddSeconds(AppSetting.ReplaySoundInterval) < System.DateTime.Now)
            {
                //
                this.Play(Program.path + AppSetting.SoundForNewOrder);
                //
                this.PlaySoundForNewOrder_time = System.DateTime.Now;
            }
        }

        void IPlaySound.PlaySoundForNewCus()
        {
            if (Program.lstNewCus.Count == 0)
            {
                this.PlaySoundForNewCus_time = DateTime.MinValue;
            }
            else if (this.PlaySoundForNewCus_time.AddSeconds(AppSetting.ReplaySoundInterval) < System.DateTime.Now)
            {
                //
                this.Play(Program.path + AppSetting.SoundForNewOrder);
                //
                this.PlaySoundForNewCus_time = System.DateTime.Now;
            }
        }

        private void Play(string sound_file)
        {
            if (sound_file == "")
            {
                return;
            }
            try
            {
                System.Media.SoundPlayer p = new System.Media.SoundPlayer();
                p.SoundLocation = sound_file;
                p.Play();
            }
            catch (Exception ex)
            {
                throw new Exception("播放提示音:" + ex.Message);
            }

        }

    }
}
