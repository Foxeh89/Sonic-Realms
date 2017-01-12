﻿using System;
using UnityEngine;

namespace SonicRealms.UI
{
    [RequireComponent(typeof(ItemCarousel))]
    public class OptionPickerGraphics : MonoBehaviour
    {
        [SerializeField]
        private OptionPickerArrowBase _nextArrow;

        [SerializeField]
        private OptionPickerArrowBase _previousArrow;

        [SerializeField]
        private OptionPickerCursorBase _focusCursor;

        private ItemCarousel _carousel;

        protected void Start()
        {
            _carousel = GetComponent<ItemCarousel>();

            _carousel.OnSelectionChange.AddListener(Carousel_OnSelectionChange);
            _carousel.OnSelectNext.AddListener(Carousel_OnSelectNext);
            _carousel.OnSelectPrevious.AddListener(Carousel_OnSelectPrevious);
            _carousel.OnFocusEnter.AddListener(Carousel_OnFocusEnter);
            _carousel.OnFocusExit.AddListener(Carousel_OnFocusExit);
            _carousel.OnItemsChanged.AddListener(Carousel_OnItemsChanged);

            UpdateArrowVisibility();
        }

        protected void OnDisable()
        {
            ExitFocus();
        }

        private void UpdateArrowVisibility()
        {
            if (_carousel.HasNext)
            {
                _nextArrow.Show();

                if (_nextArrow.IsFocused)
                    _nextArrow.Focus();
            }
            else
            {
                _nextArrow.Hide();
            }

            if (_carousel.HasPrevious)
            {
                _previousArrow.Show();

                if (_previousArrow.IsFocused)
                    _previousArrow.Focus();
            }
            else
            {
                _previousArrow.Hide();
            }
        }

        private void EnterFocus()
        {
            _nextArrow.Focus();
            _previousArrow.Focus();
            _focusCursor.Show();
        }

        private void ExitFocus()
        {
            _nextArrow.Unfocus();
            _previousArrow.Unfocus();
            _focusCursor.Hide();
        }

        private void Carousel_OnSelectionChange(ItemCarousel.SelectionChangedEvent.Args e)
        {
            _focusCursor.ChangeSelection();
        }

        private void Carousel_OnSelectPrevious()
        {
            _previousArrow.Press();
        }

        private void Carousel_OnSelectNext()
        {
            _nextArrow.Press();
        }

        private void Carousel_OnFocusEnter()
        {
            EnterFocus();
        }

        private void Carousel_OnFocusExit()
        {
            ExitFocus();
        }

        private void Carousel_OnItemsChanged()
        {
            UpdateArrowVisibility();
        }
    }
}