using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace mdb_testSW
{
    class DeviceCurrent : State
    {
        string device_current;
        public override void Handle(MDB_BOARD board)
        {

            //throw new NotImplementedException();
            if (board.NucleoPort == null)
                GoToNextState(board, false);
            board.SendToNucleo("1");
            Thread.Sleep(100);
            if (board.NucleoMessage != null)
            {
                device_current = board.NucleoMessage;
                board.BoardCurrent = Regex.Match(board.NucleoMessage, @"\d+").Value;
                Debug.WriteLine(board.BoardCurrent + "mA");
                board.SetMDBSupply();
            }
            if (board.BoardCurrent != null)
            {
                GoToNextState(board, true);
            }

        }

        public override void GoToNextState(MDB_BOARD board, bool state)
        {
            state_number = 4;
            board.test_result[state_number] = state ? 1 : 0;
            board.UpdateList(state_number, state);

            if (board.MDB_MOD)
            {
                board.test_result[5] = state ? 1 : 0;
                board.BoardTestStatus = state ? 1 : 0;
                board.State = new SQL_Update();
            }
            else if (state)
            {
                board.UpdateMessage = "Device Current: " + device_current + "mA";
                board.State = new Led_State();
            }
            else
            {
                board.BoardErrorDescription = "Failed to Read Device Current";
                board.UpdateMessage = "Failed to Read Device Current";
                board.State = new ErrorState();
            }

        }
    }
}
