using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Repository.Publication
{
  public   interface IPublicationJob
    {
        void PrintTime();
        void CheckPublicationStatus();
        void CheckPublicationCount();
        //  void CheckPublicationStatus();
    }
}
