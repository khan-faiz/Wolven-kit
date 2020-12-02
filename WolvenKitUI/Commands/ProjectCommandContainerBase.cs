﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectCommandContainerBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System;
using Catel.IoC;
using Catel.Services;
using WolvenKit.App.Model;
using WolvenKit.Common.Services;

namespace WolvenKitUI
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Catel.Threading;
    using Orc.ProjectManagement;

    public abstract class ProjectCommandContainerBase : CommandContainerBase
    {
        #region Fields
        protected readonly ICommandManager _commandManager;
        protected readonly ILoggerService _logger;
        protected readonly IProjectManager _projectManager;
        protected readonly IPleaseWaitService _pleaseWaitService;
        #endregion

        #region Constructors
        protected ProjectCommandContainerBase(string commandName, 
            ICommandManager commandManager, 
            IProjectManager projectManager,
            ILoggerService loggerService)
            : base(commandName, commandManager)
        {
            Argument.IsNotNull(() => projectManager);
            Argument.IsNotNull(() => loggerService);

            _commandManager = commandManager;
            _projectManager = projectManager;
            _logger = loggerService;
            _pleaseWaitService = ServiceLocator.Default.ResolveType<IPleaseWaitService>();

            _projectManager.ProjectActivatedAsync += OnProjectActivatedAsync;
        }
        #endregion

        #region Methods
        private Task OnProjectActivatedAsync(object sender, ProjectUpdatedEventArgs e)
        {
            //await Task.Run(() => ProjectActivated((Project) e.OldProject, (Project) e.NewProject));
            ProjectActivated((Project)e.OldProject, (Project)e.NewProject);

            //TODO: why is that here?
            _commandManager.InvalidateCommands();

            return TaskHelper.Completed;
        }

        protected virtual async Task ProjectActivated(Project oldProject, Project newProject)
        {
            if (newProject == null)
                return;

            // TODO: move this to somewhere more global
            // however, we want to unload the bundled from memory probably when a mod cp77 project is loaded and vice-versa
            // ... not sure

            await Task.Run(() => newProject.Initialize());
        }

        protected override bool CanExecute(object parameter)
        {
            if (_projectManager.ActiveProject == null)
            {
                return false;
            }

            return base.CanExecute(parameter);
        }
        #endregion
    }
}