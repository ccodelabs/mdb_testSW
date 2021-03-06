﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mdb_testSW
{
    class RS232 : State
    {
        public override void GoToNextState(MDB_BOARD board, bool state)
        {
            state_number = 3;
            board.test_result[state_number] = state ? 1 : 0;
            board.UpdateList(state_number, state);

            if (state)
            {
                board.UpdateMessage = "RS232 Test Passed";
                board.State = new DeviceCurrent();
            }
            else
            {
                board.BoardErrorDescription = "RS232 Test Failed";
                board.UpdateMessage = "RS232 Test Failed";
                board.State = new ErrorState();
            }

        }

        public override void Handle(MDB_BOARD board)
        {
            Debug.WriteLine("RS232 State " + board.RS232Port);

            string[] lines = board.Start_script("--com " + board.RS232Port);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("FAILED"))
                {
                    if (lines[i - 1].Contains("SERIAL") || lines[i - 1].Contains("[M <]checking MDB S -> M") || (lines[i - 1].Contains("[M <]checking MDB S -> M")))
                    {
                        Debug.WriteLine("Serial failed");
                        GoToNextState(board, false);
                        return;
                    }
                }
            }
            Thread.Sleep(100);
            GoToNextState(board, true);
            Debug.WriteLine("RS232 Sucess");
            return;
        }
    }
}
