﻿using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class ViewModelTests
    {
        [Test]
        public void PropertyTest()
        {
            FolderViewModel VM = new FolderViewModel();
            VM.imgPath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\MapResponses\Wien-Linz.jpg";
            NUnit.Framework.Assert.NotNull(VM.imgPath);
        }
    }
}
