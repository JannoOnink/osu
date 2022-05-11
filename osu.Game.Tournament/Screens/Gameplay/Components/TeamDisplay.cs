// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Tournament.Components;
using osu.Game.Tournament.Models;
using osuTK;
using System;

namespace osu.Game.Tournament.Screens.Gameplay.Components
{
    public class TeamDisplay : DrawableTournamentTeam
    {
        private readonly TeamScore score;
        private TournamentSpriteTextWithBackground pickemsSprite;

        private bool showScore;

        public bool ShowScore
        {
            get => showScore;
            set
            {
                if (showScore == value)
                    return;

                showScore = value;

                if (IsLoaded)
                    updateDisplay();
            }
        }

        public TeamDisplay(TournamentTeam team, TournamentMatch match, TeamColour colour, Bindable<int?> currentTeamScore, int pointsToWin)
            : base(team)
        {
            AutoSizeAxes = Axes.Both;

            bool flip = colour == TeamColour.Red;

            var anchor = flip ? Anchor.TopLeft : Anchor.TopRight;

            Flag.RelativeSizeAxes = Axes.None;
            Flag.Scale = new Vector2(0.8f);
            Flag.Origin = anchor;
            Flag.Anchor = anchor;

            Margin = new MarginPadding(20);

            pickemsSprite = new TournamentSpriteTextWithBackground("")
            {
                Scale = new Vector2(0.3f),
                Origin = anchor,
                Anchor = anchor
            };

            team.PickemsRate.ValueChanged += val =>
            {
                pickemsSprite.Text.Text = $"Pickem rate: {Math.Round(val.NewValue, 2).ToString()}%";
            };

            InternalChild = new Container
            {
                AutoSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new FillFlowContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Direction = FillDirection.Horizontal,
                        Spacing = new Vector2(5),
                        Children = new Drawable[]
                        {
                            Flag,
                            new FillFlowContainer
                            {
                                AutoSizeAxes = Axes.Both,
                                Direction = FillDirection.Vertical,
                                Origin = anchor,
                                Anchor = anchor,
                                Spacing = new Vector2(5),
                                Children = new Drawable[]
                                {
                                    new FillFlowContainer
                                    {
                                        AutoSizeAxes = Axes.Both,
                                        Direction = FillDirection.Horizontal,
                                        Spacing = new Vector2(5),
                                        Origin = anchor,
                                        Anchor = anchor,
                                        Children = new Drawable[]
                                        {
                                            new DrawableTeamHeader(colour)
                                            {
                                                Scale = new Vector2(0.75f),
                                                Origin = anchor,
                                                Anchor = anchor,
                                            },
                                            score = new TeamScore(currentTeamScore, colour, pointsToWin)
                                            {
                                                Origin = anchor,
                                                Anchor = anchor,
                                            }
                                        }
                                    },
                                    new FillFlowContainer {
                                        AutoSizeAxes = Axes.Both,
                                        Direction= FillDirection.Horizontal,
                                        Spacing= new Vector2(5),
                                        Origin= anchor,
                                        Anchor= anchor,
                                        Children= new Drawable[]
                                        {
                                            new TournamentSpriteTextWithBackground(team?.FullName.Value ?? "???")
                                            {
                                                Scale = new Vector2(0.5f),
                                                Origin = anchor,
                                                Anchor = anchor,
                                            },
                                            new TournamentSpriteTextWithBackground(match?.PicksBans?.)
                                        }
                                    },
                                    pickemsSprite
                                }
                            },
                        }
                    },
                }
            };
        }

        private string getPlayersBans(TournamentMatch match, TeamColour colour)
        {
            string[] maps = new string[2];
            foreach (var e in match.PicksBans)
            {
                if (e.Team == colour)
                {
                    maps.P
                }
            }
            return "";
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updateDisplay();
            FinishTransforms(true);
        }

        private void updateDisplay()
        {
            score.FadeTo(ShowScore ? 1 : 0, 200);
        }
    }
}
