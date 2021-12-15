using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;

namespace MakeDPCM
{
	class WavePlayer
	{
		private SoundPlayer _player = new SoundPlayer();
		private MemoryStream _stream = null;
		private WaveFile _wf = null;

		~WavePlayer()
		{
			if (_player != null)
			{
				_player.Dispose();
				_player = null;
			}
			if (_stream != null)
			{
				_stream.Dispose();
				_stream = null;
			}
			_wf = null;
			GC.Collect();
		}

		public void Play()
		{
			if (_player.Stream == null) return;
			_player.Stop();
			_player.Play();
		}
		public void Stop()
		{
			_player.Stop();
		}

		public void SetWaveFile(WaveFile wf)
		{
			SetWaveFile(wf, wf.Rate);
		}
		public void SetWaveFile(WaveFile wf, double rate)
		{
			if (wf == null) return;
			if (rate <= 0.0) return;
			if (_wf != null && _wf.Equals(wf) && _wf.Rate == rate)
				return;

			_wf = wf;
			_wf.Rate = rate;

			_player.Stop();

			if (_stream != null)
			{
				_stream.Dispose();
				_stream = null;
			}

			byte[] buf = _wf.GetFileByteArray();

			_stream = new MemoryStream(buf);
			_player.Stream = _stream;
			_player.Load();

			buf = null;
			GC.Collect();
		}
	}
}
