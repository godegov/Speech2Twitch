using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Speech2Twitch
{
	public partial class TwitchConnectionOptions : Form
	{
		public delegate void OnSetNewValues(DataStructures.TwitchConnectionDescriptor inTwitchConnectionDescriptor);

		private OnSetNewValues NewValuesSet;
		public TwitchConnectionOptions(ref DataStructures.TwitchConnectionDescriptor inTwitchConnectionDescriptor, OnSetNewValues InNewValuesSet)
		{
			NewValuesSet = InNewValuesSet;
			InitializeComponent();
			this.ChannelName.Text = inTwitchConnectionDescriptor.StreamChannelName;
			this.UserLogin.Text = inTwitchConnectionDescriptor.LoginName;
			this.Token.Text = inTwitchConnectionDescriptor.TwitchToken;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DataStructures.TwitchConnectionDescriptor tmp = new DataStructures.TwitchConnectionDescriptor();
			tmp.StreamChannelName = this.ChannelName.Text;
			tmp.LoginName = this.UserLogin.Text;
			tmp.TwitchToken = this.Token.Text;
			NewValuesSet(tmp);
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
