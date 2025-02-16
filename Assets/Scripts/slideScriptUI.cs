using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slideScriptUI : MonoBehaviour
{
    public ScrollRect scrollRect; // Reference to the ScrollRect
    public int totalPages = 1; // Total number of pages
    public float scrollSpeed = 5f; // Speed of scrolling

    private int currentPage = 0; // Current page index (0-based)
    private bool isScrolling = false;
    private float targetPosition;

    public Button nextButton;
    public Button previousButton;

    private void Update()
    {
        if (isScrolling)
        {
            float currentPos = scrollRect.horizontalNormalizedPosition;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(currentPos, targetPosition, Time.deltaTime * scrollSpeed);

            if (Mathf.Abs(currentPos - targetPosition) < 0.001f)
            {
                scrollRect.horizontalNormalizedPosition = targetPosition;
                isScrolling = false;
            }
        }

        UpdateButtonStates();
    }

    public void ScrollNext()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            ScrollTo(currentPage);
        }
    }

    public void ScrollPrevious()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ScrollTo(currentPage);
        }
    }

    private void ScrollTo(int pageIndex)
    {
        targetPosition = (float)pageIndex / (totalPages - 1);
        isScrolling = true;
    }

    private void UpdateButtonStates()
    {
        // Enable or disable buttons based on the current page
        if (previousButton != null)
            previousButton.interactable = currentPage > 0;

        if (nextButton != null)
            nextButton.interactable = currentPage < totalPages - 1;
    }
}
