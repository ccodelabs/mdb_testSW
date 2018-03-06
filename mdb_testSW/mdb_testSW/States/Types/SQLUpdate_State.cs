using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdb_testSW
{
    class SQL_Update : State
    {
        public override void GoToNextState(MDB_BOARD board, bool state)
        {

            board.State = new FinishState();
        }

        public override void Handle(MDB_BOARD board)
        {
            board.UpdateMessage = "Saving into database";
            if (board.NucleoPort != null)
                board.CloseSerialPort();

            Debug.WriteLine("Update DB");

            board.InsertTestResult();

            GoToNextState(board, true);
        }
    }
}
