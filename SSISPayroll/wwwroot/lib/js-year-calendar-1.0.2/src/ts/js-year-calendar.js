"use strict";
/* =========================================================
 * JS year calendar v1.0.0
 * Repo: https://github.com/year-calendar/js-year-calendar
 * =========================================================
 * Created by Paul David-Sivelle
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================= */
exports.__esModule = true;
// NodeList forEach() polyfill
if (typeof NodeList !== "undefined" && !NodeList.prototype.forEach) {
    NodeList.prototype.forEach = function (callback, thisArg) {
        thisArg = thisArg || window;
        for (var i = 0; i < this.length; i++) {
            callback.call(thisArg, this[i], i, this);
        }
    };
}
// Element closest() polyfill
if (typeof Element !== "undefined" && !Element.prototype.matches) {
    var prototype = Element.prototype;
    Element.prototype.matches = prototype.msMatchesSelector || prototype.webkitMatchesSelector;
}
if (typeof Element !== "undefined" && !Element.prototype.closest) {
    Element.prototype.closest = function (s) {
        var el = this;
        if (!document.documentElement.contains(el))
            return null;
        do {
            if (el.matches(s))
                return el;
            el = el.parentElement || el.parentNode;
        } while (el !== null && el.nodeType == 1);
        return null;
    };
}
/**
 * Calendar instance.
 */
var Calendar = /** @class */ (function () {
    /**
     * Create a new calendar.
     * @param element The element (or the selector to an element) in which the calendar should be created.
     * @param options [Optional] The options used to customize the calendar
     */
    function Calendar(element, options) {
        if (options === void 0) { options = null; }
        if (element instanceof HTMLElement) {
            this.element = element;
        }
        else if (typeof element === "string") {
            this.element = document.querySelector(element);
        }
        else {
            throw new Error("The element parameter should be a DOM node or a selector");
        }
        this.element.classList.add('calendar');
        this._initializeEvents(options);
        this._initializeOptions(options);
        this.setYear(this.options.startYear);
    }
    Calendar.prototype._initializeOptions = function (opt) {
        if (opt == null) {
            opt = {};
        }
        this.options = {
            startYear: !isNaN(parseInt(opt.startYear)) ? parseInt(opt.startYear) : new Date().getFullYear(),
            minDate: opt.minDate instanceof Date ? opt.minDate : null,
            maxDate: opt.maxDate instanceof Date ? opt.maxDate : null,
            language: (opt.language != null && Calendar.locales[opt.language] != null) ? opt.language : 'en',
            allowOverlap: opt.allowOverlap != null ? opt.allowOverlap : true,
            displayWeekNumber: opt.displayWeekNumber != null ? opt.displayWeekNumber : false,
            displayDisabledDataSource: opt.displayDisabledDataSource != null ? opt.displayDisabledDataSource : false,
            displayHeader: opt.displayHeader != null ? opt.displayHeader : true,
            alwaysHalfDay: opt.alwaysHalfDay != null ? opt.alwaysHalfDay : false,
            enableRangeSelection: opt.enableRangeSelection != null ? opt.enableRangeSelection : false,
            disabledDays: opt.disabledDays instanceof Array ? opt.disabledDays : [],
            disabledWeekDays: opt.disabledWeekDays instanceof Array ? opt.disabledWeekDays : [],
            hiddenWeekDays: opt.hiddenWeekDays instanceof Array ? opt.hiddenWeekDays : [],
            roundRangeLimits: opt.roundRangeLimits != null ? opt.roundRangeLimits : false,
            dataSource: opt.dataSource instanceof Array || typeof opt.dataSource === "function" ? opt.dataSource : [],
            style: opt.style == 'background' || opt.style == 'border' || opt.style == 'custom' ? opt.style : 'border',
            enableContextMenu: opt.enableContextMenu != null ? opt.enableContextMenu : false,
            contextMenuItems: opt.contextMenuItems instanceof Array ? opt.contextMenuItems : [],
            customDayRenderer: typeof opt.customDayRenderer === "function" ? opt.customDayRenderer : null,
            customDataSourceRenderer: typeof opt.customDataSourceRenderer === "function" ? opt.customDataSourceRenderer : null,
            weekStart: !isNaN(parseInt(opt.weekStart)) ? parseInt(opt.weekStart) : null,
            loadingTemplate: typeof opt.loadingTemplate === "string" || opt.loadingTemplate instanceof HTMLElement ? opt.loadingTemplate : null
        };
        if (this.options.dataSource instanceof Array) {
            this._dataSource = this.options.dataSource;
            this._initializeDatasourceColors();
        }
    };
    Calendar.prototype._initializeEvents = function (opt) {
        if (opt == null) {
            opt = [];
        }
        if (opt.yearChanged) {
            this.element.addEventListener('yearChanged', opt.yearChanged);
        }
        if (opt.renderEnd) {
            this.element.addEventListener('renderEnd', opt.renderEnd);
        }
        if (opt.clickDay) {
            this.element.addEventListener('clickDay', opt.clickDay);
        }
        if (opt.dayContextMenu) {
            this.element.addEventListener('dayContextMenu', opt.dayContextMenu);
        }
        if (opt.selectRange) {
            this.element.addEventListener('selectRange', opt.selectRange);
        }
        if (opt.mouseOnDay) {
            this.element.addEventListener('mouseOnDay', opt.mouseOnDay);
        }
        if (opt.mouseOutDay) {
            this.element.addEventListener('mouseOutDay', opt.mouseOutDay);
        }
    };
    Calendar.prototype._fetchDataSource = function (callback) {
        if (typeof this.options.dataSource === "function") {
            var getDataSource = this.options.dataSource;
            if (getDataSource.length == 2) {
                // 2 parameters, means callback method
                getDataSource(this.options.startYear, callback);
            }
            else {
                // 1 parameter, means synchronous or promise method
                var result = getDataSource(this.options.startYear);
                if (result instanceof Array) {
                    callback(result);
                }
                else {
                    result.then(callback);
                }
            }
        }
        else {
            callback(this.options.dataSource);
        }
    };
    Calendar.prototype._initializeDatasourceColors = function () {
        for (var i = 0; i < this._dataSource.length; i++) {
            if (this._dataSource[i].color == null) {
                this._dataSource[i].color = Calendar.colors[i % Calendar.colors.length];
            }
        }
    };
    /**
     * Renders the calendar.
     */
    Calendar.prototype.render = function (isLoading) {
        if (isLoading === void 0) { isLoading = false; }
        // Clear the calendar (faster method)
        while (this.element.firstChild) {
            this.element.removeChild(this.element.firstChild);
        }
        if (this.options.displayHeader) {
            this._renderHeader();
        }
        if (isLoading) {
            this._renderLoading();
        }
        else {
            this._renderBody();
            this._renderDataSource();
            this._applyEvents();
            // Fade animation
            var months = this.element.querySelector('.months-container');
            months.style.opacity = '0';
            months.style.display = 'block';
            months.style.transition = 'opacity 0.5s';
            setTimeout(function () {
                months.style.opacity = '1';
                setTimeout(function () { return months.style.transition = ''; }, 500);
            }, 0);
            this._triggerEvent('renderEnd', { currentYear: this.options.startYear });
        }
    };
    Calendar.prototype._renderHeader = function () {
        var header = document.createElement('div');
        header.classList.add('calendar-header');
        var headerTable = document.createElement('table');
        var prevDiv = document.createElement('th');
        prevDiv.classList.add('prev');
        if (this.options.minDate != null && this.options.minDate > new Date(this.options.startYear - 1, 11, 31)) {
            prevDiv.classList.add('disabled');
        }
        var prevIcon = document.createElement('span');
        prevIcon.innerHTML = "&lsaquo;";
        prevDiv.appendChild(prevIcon);
        headerTable.appendChild(prevDiv);
        var prev2YearDiv = document.createElement('th');
        prev2YearDiv.classList.add('year-title');
        prev2YearDiv.classList.add('year-neighbor2');
        prev2YearDiv.textContent = (this.options.startYear - 2).toString();
        if (this.options.minDate != null && this.options.minDate > new Date(this.options.startYear - 2, 11, 31)) {
            prev2YearDiv.classList.add('disabled');
        }
        headerTable.appendChild(prev2YearDiv);
        var prevYearDiv = document.createElement('th');
        prevYearDiv.classList.add('year-title');
        prevYearDiv.classList.add('year-neighbor');
        prevYearDiv.textContent = (this.options.startYear - 1).toString();
        if (this.options.minDate != null && this.options.minDate > new Date(this.options.startYear - 1, 11, 31)) {
            prevYearDiv.classList.add('disabled');
        }
        headerTable.appendChild(prevYearDiv);
        var yearDiv = document.createElement('th');
        yearDiv.classList.add('year-title');
        yearDiv.textContent = this.options.startYear.toString();
        headerTable.appendChild(yearDiv);
        var nextYearDiv = document.createElement('th');
        nextYearDiv.classList.add('year-title');
        nextYearDiv.classList.add('year-neighbor');
        nextYearDiv.textContent = (this.options.startYear + 1).toString();
        if (this.options.maxDate != null && this.options.maxDate < new Date(this.options.startYear + 1, 0, 1)) {
            nextYearDiv.classList.add('disabled');
        }
        headerTable.appendChild(nextYearDiv);
        var next2YearDiv = document.createElement('th');
        next2YearDiv.classList.add('year-title');
        next2YearDiv.classList.add('year-neighbor2');
        next2YearDiv.textContent = (this.options.startYear + 2).toString();
        if (this.options.maxDate != null && this.options.maxDate < new Date(this.options.startYear + 2, 0, 1)) {
            next2YearDiv.classList.add('disabled');
        }
        headerTable.appendChild(next2YearDiv);
        var nextDiv = document.createElement('th');
        nextDiv.classList.add('next');
        if (this.options.maxDate != null && this.options.maxDate < new Date(this.options.startYear + 1, 0, 1)) {
            nextDiv.classList.add('disabled');
        }
        var nextIcon = document.createElement('span');
        nextIcon.innerHTML = "&rsaquo;";
        nextDiv.appendChild(nextIcon);
        headerTable.appendChild(nextDiv);
        header.appendChild(headerTable);
        this.element.appendChild(header);
    };
    Calendar.prototype._renderBody = function () {
        var monthsDiv = document.createElement('div');
        monthsDiv.classList.add('months-container');
        for (var m = 0; m < 12; m++) {
            /* Container */
            var monthDiv = document.createElement('div');
            monthDiv.classList.add('month-container');
            monthDiv.dataset.monthId = m.toString();
            if (this._nbCols) {
                monthDiv.classList.add("month-" + this._nbCols);
            }
            var firstDate = new Date(this.options.startYear, m, 1);
            var table = document.createElement('table');
            table.classList.add('month');
            /* Month header */
            var thead = document.createElement('thead');
            var titleRow = document.createElement('tr');
            var titleCell = document.createElement('th');
            titleCell.classList.add('month-title');
            titleCell.setAttribute('colspan', this.options.displayWeekNumber ? '8' : '7');
            titleCell.textContent = Calendar.locales[this.options.language].months[m];
            titleRow.appendChild(titleCell);
            thead.appendChild(titleRow);
            var headerRow = document.createElement('tr');
            if (this.options.displayWeekNumber) {
                var weekNumberCell = document.createElement('th');
                weekNumberCell.classList.add('week-number');
                weekNumberCell.textContent = Calendar.locales[this.options.language].weekShort;
                headerRow.appendChild(weekNumberCell);
            }
            var weekStart = this.options.weekStart ? this.options.weekStart : Calendar.locales[this.options.language].weekStart;
            var d = weekStart;
            do {
                var headerCell = document.createElement('th');
                headerCell.classList.add('day-header');
                headerCell.textContent = Calendar.locales[this.options.language].daysMin[d];
                if (this._isHidden(d)) {
                    headerCell.classList.add('hidden');
                }
                headerRow.appendChild(headerCell);
                d++;
                if (d >= 7)
                    d = 0;
            } while (d != weekStart);
            thead.appendChild(headerRow);
            table.appendChild(thead);
            /* Days */
            var currentDate = new Date(firstDate.getTime());
            var lastDate = new Date(this.options.startYear, m + 1, 0);
            while (currentDate.getDay() != weekStart) {
                currentDate.setDate(currentDate.getDate() - 1);
            }
            while (currentDate <= lastDate) {
                var row = document.createElement('tr');
                if (this.options.displayWeekNumber) {
                    var weekNumberCell = document.createElement('td');
                    var currentThursday = new Date(currentDate.getTime()); // Week number is computed based on the thursday
                    currentThursday.setDate(currentThursday.getDate() - weekStart + 4);
                    weekNumberCell.classList.add('week-number');
                    weekNumberCell.textContent = this.getWeekNumber(currentThursday).toString();
                    row.appendChild(weekNumberCell);
                }
                do {
                    var cell = document.createElement('td');
                    cell.classList.add('day');
                    if (this._isHidden(currentDate.getDay())) {
                        cell.classList.add('hidden');
                    }
                    if (currentDate < firstDate) {
                        cell.classList.add('old');
                    }
                    else if (currentDate > lastDate) {
                        cell.classList.add('new');
                    }
                    else {
                        if (this._isDisabled(currentDate)) {
                            cell.classList.add('disabled');
                        }
                        var cellContent = document.createElement('div');
                        cellContent.classList.add('day-content');
                        cellContent.textContent = currentDate.getDate().toString();
                        cell.appendChild(cellContent);
                        if (this.options.customDayRenderer) {
                            this.options.customDayRenderer(cellContent, currentDate);
                        }
                    }
                    row.appendChild(cell);
                    currentDate.setDate(currentDate.getDate() + 1);
                } while (currentDate.getDay() != weekStart);
                table.appendChild(row);
            }
            monthDiv.appendChild(table);
            monthsDiv.appendChild(monthDiv);
        }
        this.element.appendChild(monthsDiv);
    };
    Calendar.prototype._renderLoading = function () {
        var container = document.createElement('div');
        container.classList.add('calendar-loading-container');
        container.style.minHeight = (this._nbCols * 200) + 'px';
        var loading = document.createElement('div');
        loading.classList.add('calendar-loading');
        if (this.options.loadingTemplate) {
            if (typeof this.options.loadingTemplate === "string") {
                loading.innerHTML = this.options.loadingTemplate;
            }
            else if (this.options.loadingTemplate instanceof HTMLElement) {
                loading.appendChild(this.options.loadingTemplate);
            }
        }
        else {
            var spinner = document.createElement('div');
            spinner.classList.add('calendar-spinner');
            for (var i = 1; i <= 3; i++) {
                var bounce = document.createElement('div');
                bounce.classList.add("bounce" + i);
                spinner.appendChild(bounce);
            }
            loading.appendChild(spinner);
        }
        container.appendChild(loading);
        this.element.appendChild(container);
    };
    Calendar.prototype._renderDataSource = function () {
        var _this = this;
        if (this._dataSource != null && this._dataSource.length > 0) {
            this.element.querySelectorAll('.month-container').forEach(function (month) {
                var monthId = parseInt(month.dataset.monthId);
                var firstDate = new Date(_this.options.startYear, monthId, 1);
                var lastDate = new Date(_this.options.startYear, monthId + 1, 1);
                if ((_this.options.minDate == null || lastDate > _this.options.minDate) && (_this.options.maxDate == null || firstDate <= _this.options.maxDate)) {
                    var monthData = [];
                    for (var i = 0; i < _this._dataSource.length; i++) {
                        if (!(_this._dataSource[i].startDate >= lastDate) || (_this._dataSource[i].endDate < firstDate)) {
                            monthData.push(_this._dataSource[i]);
                        }
                    }
                    if (monthData.length > 0) {
                        month.querySelectorAll('.day-content').forEach(function (day) {
                            var currentDate = new Date(_this.options.startYear, monthId, parseInt(day.textContent));
                            var nextDate = new Date(_this.options.startYear, monthId, currentDate.getDate() + 1);
                            var dayData = [];
                            if ((_this.options.minDate == null || currentDate >= _this.options.minDate) && (_this.options.maxDate == null || currentDate <= _this.options.maxDate)) {
                                for (var i = 0; i < monthData.length; i++) {
                                    if (monthData[i].startDate < nextDate && monthData[i].endDate >= currentDate) {
                                        dayData.push(monthData[i]);
                                    }
                                }
                                if (dayData.length > 0 && (_this.options.displayDisabledDataSource || !_this._isDisabled(currentDate))) {
                                    _this._renderDataSourceDay(day, currentDate, dayData);
                                }
                            }
                        });
                    }
                }
            });
        }
    };
    Calendar.prototype._renderDataSourceDay = function (elt, currentDate, events) {
        var parent = elt.parentElement;
        switch (this.options.style) {
            case 'border':
                var weight = 0;
                if (events.length == 1) {
                    weight = 4;
                }
                else if (events.length <= 3) {
                    weight = 2;
                }
                else {
                    parent.style.boxShadow = 'inset 0 -4px 0 0 black';
                }
                if (weight > 0) {
                    var boxShadow = '';
                    for (var i = 0; i < events.length; i++) {
                        if (boxShadow != '') {
                            boxShadow += ",";
                        }
                        boxShadow += "inset 0 -" + (i + 1) * weight + "px 0 0 " + events[i].color;
                    }
                    parent.style.boxShadow = boxShadow;
                }
                break;
            case 'background':
                parent.style.backgroundColor = events[events.length - 1].color;
                var currentTime = currentDate.getTime();
                if (events[events.length - 1].startDate.getTime() == currentTime) {
                    parent.classList.add('day-start');
                    if (events[events.length - 1].startHalfDay || this.options.alwaysHalfDay) {
                        parent.classList.add('day-half');
                        // Find color for other half
                        var otherColor = 'transparent';
                        for (var i = events.length - 2; i >= 0; i--) {
                            if (events[i].startDate.getTime() != currentTime || (!events[i].startHalfDay && !this.options.alwaysHalfDay)) {
                                otherColor = events[i].color;
                                break;
                            }
                        }
                        parent.style.background = "linear-gradient(-45deg, " + events[events.length - 1].color + ", " + events[events.length - 1].color + " 49%, " + otherColor + " 51%, " + otherColor + ")";
                    }
                    else if (this.options.roundRangeLimits) {
                        parent.classList.add('round-left');
                    }
                }
                else if (events[events.length - 1].endDate.getTime() == currentTime) {
                    parent.classList.add('day-end');
                    if (events[events.length - 1].endHalfDay || this.options.alwaysHalfDay) {
                        parent.classList.add('day-half');
                        // Find color for other half
                        var otherColor = 'transparent';
                        for (var i = events.length - 2; i >= 0; i--) {
                            if (events[i].endDate.getTime() != currentTime || (!events[i].endHalfDay && !this.options.alwaysHalfDay)) {
                                otherColor = events[i].color;
                                break;
                            }
                        }
                        parent.style.background = "linear-gradient(135deg, " + events[events.length - 1].color + ", " + events[events.length - 1].color + " 49%, " + otherColor + " 51%, " + otherColor + ")";
                    }
                    else if (this.options.roundRangeLimits) {
                        parent.classList.add('round-right');
                    }
                }
                break;
            case 'custom':
                if (this.options.customDataSourceRenderer) {
                    this.options.customDataSourceRenderer.call(this, elt, currentDate, events);
                }
                break;
        }
    };
    Calendar.prototype._applyEvents = function () {
        var _this = this;
        if (this.options.displayHeader) {
            /* Header buttons */
            this.element.querySelectorAll('.year-neighbor, .year-neighbor2').forEach(function (element) {
                element.addEventListener('click', function (e) {
                    if (!e.currentTarget.classList.contains('disabled')) {
                        _this.setYear(parseInt(e.currentTarget.textContent));
                    }
                });
            });
            this.element.querySelector('.calendar-header .prev').addEventListener('click', function (e) {
                if (!e.currentTarget.classList.contains('disabled')) {
                    var months = _this.element.querySelector('.months-container');
                    months.style.transition = 'margin-left 0.1s';
                    months.style.marginLeft = '100%';
                    setTimeout(function () {
                        months.style.visibility = 'hidden';
                        months.style.transition = '';
                        months.style.marginLeft = '0';
                        setTimeout(function () {
                            _this.setYear(_this.options.startYear - 1);
                        }, 50);
                    }, 100);
                }
            });
            this.element.querySelector('.calendar-header .next').addEventListener('click', function (e) {
                if (!e.currentTarget.classList.contains('disabled')) {
                    var months = _this.element.querySelector('.months-container');
                    months.style.transition = 'margin-left 0.1s';
                    months.style.marginLeft = '-100%';
                    setTimeout(function () {
                        months.style.visibility = 'hidden';
                        months.style.transition = '';
                        months.style.marginLeft = '0';
                        setTimeout(function () {
                            _this.setYear(_this.options.startYear + 1);
                        }, 50);
                    }, 100);
                }
            });
        }
        var cells = this.element.querySelectorAll('.day:not(.old):not(.new):not(.disabled)');
        cells.forEach(function (cell) {
            /* Click on date */
            cell.addEventListener('click', function (e) {
                e.stopPropagation();
                var date = _this._getDate(e.currentTarget);
                _this._triggerEvent('clickDay', {
                    element: e.currentTarget,
                    date: date,
                    events: _this.getEvents(date)
                });
            });
            /* Click right on date */
            cell.addEventListener('contextmenu', function (e) {
                if (_this.options.enableContextMenu) {
                    e.preventDefault();
                    if (_this.options.contextMenuItems.length > 0) {
                        _this._openContextMenu(e.currentTarget);
                    }
                }
                var date = _this._getDate(e.currentTarget);
                _this._triggerEvent('dayContextMenu', {
                    element: e.currentTarget,
                    date: date,
                    events: _this.getEvents(date)
                });
            });
            /* Range selection */
            if (_this.options.enableRangeSelection) {
                cell.addEventListener('mousedown', function (e) {
                    if (e.which == 1) {
                        var currentDate = _this._getDate(e.currentTarget);
                        if (_this.options.allowOverlap || _this.isThereFreeSlot(currentDate)) {
                            _this._mouseDown = true;
                            _this._rangeStart = _this._rangeEnd = currentDate;
                            _this._refreshRange();
                        }
                    }
                });
                cell.addEventListener('mouseenter', function (e) {
                    if (_this._mouseDown) {
                        var currentDate = _this._getDate(e.currentTarget);
                        if (!_this.options.allowOverlap) {
                            var newDate = new Date(_this._rangeStart.getTime());
                            if (newDate < currentDate) {
                                var nextDate = new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate() + 1);
                                while (newDate < currentDate) {
                                    if (!_this.isThereFreeSlot(nextDate, false)) {
                                        break;
                                    }
                                    newDate.setDate(newDate.getDate() + 1);
                                    nextDate.setDate(nextDate.getDate() + 1);
                                }
                            }
                            else {
                                var nextDate = new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate() - 1);
                                while (newDate > currentDate) {
                                    if (!_this.isThereFreeSlot(nextDate, true)) {
                                        break;
                                    }
                                    newDate.setDate(newDate.getDate() - 1);
                                    nextDate.setDate(nextDate.getDate() - 1);
                                }
                            }
                            currentDate = newDate;
                        }
                        var oldValue = _this._rangeEnd;
                        _this._rangeEnd = currentDate;
                        if (oldValue.getTime() != _this._rangeEnd.getTime()) {
                            _this._refreshRange();
                        }
                    }
                });
            }
            /* Hover date */
            cell.addEventListener('mouseenter', function (e) {
                if (!_this._mouseDown) {
                    var date = _this._getDate(e.currentTarget);
                    _this._triggerEvent('mouseOnDay', {
                        element: e.currentTarget,
                        date: date,
                        events: _this.getEvents(date)
                    });
                }
            });
            cell.addEventListener('mouseleave', function (e) {
                var date = _this._getDate(e.currentTarget);
                _this._triggerEvent('mouseOutDay', {
                    element: e.currentTarget,
                    date: date,
                    events: _this.getEvents(date)
                });
            });
        });
        if (this.options.enableRangeSelection) {
            // Release range selection
            window.addEventListener('mouseup', function (e) {
                if (_this._mouseDown) {
                    _this._mouseDown = false;
                    _this._refreshRange();
                    var minDate = _this._rangeStart < _this._rangeEnd ? _this._rangeStart : _this._rangeEnd;
                    var maxDate = _this._rangeEnd > _this._rangeStart ? _this._rangeEnd : _this._rangeStart;
                    _this._triggerEvent('selectRange', {
                        startDate: minDate,
                        endDate: maxDate,
                        events: _this.getEventsOnRange(minDate, new Date(maxDate.getFullYear(), maxDate.getMonth(), maxDate.getDate() + 1))
                    });
                }
            });
        }
        /* Responsive management */
        if (this._responsiveInterval) {
            clearInterval(this._responsiveInterval);
            this._responsiveInterval = null;
        }
        this._responsiveInterval = setInterval(function () {
            if (_this.element.querySelector('.month') == null) {
                return;
            }
            var calendarSize = _this.element.offsetWidth;
            var monthSize = _this.element.querySelector('.month').offsetWidth + 10;
            _this._nbCols = null;
            if (monthSize * 6 < calendarSize) {
                _this._nbCols = 2;
            }
            else if (monthSize * 4 < calendarSize) {
                _this._nbCols = 3;
            }
            else if (monthSize * 3 < calendarSize) {
                _this._nbCols = 4;
            }
            else if (monthSize * 2 < calendarSize) {
                _this._nbCols = 6;
            }
            else {
                _this._nbCols = 12;
            }
            _this.element.querySelectorAll('.month-container').forEach(function (month) {
                if (!month.classList.contains("month-" + _this._nbCols)) {
                    ['month-2', 'month-3', 'month-4', 'month-6', 'month-12'].forEach(function (className) {
                        month.classList.remove(className);
                    });
                    month.classList.add("month-" + _this._nbCols);
                }
            });
        }, 300);
    };
    Calendar.prototype._refreshRange = function () {
        var _this = this;
        this.element.querySelectorAll('td.day.range').forEach(function (day) { return day.classList.remove('range'); });
        this.element.querySelectorAll('td.day.range-start').forEach(function (day) { return day.classList.remove('range-start'); });
        this.element.querySelectorAll('td.day.range-end').forEach(function (day) { return day.classList.remove('range-end'); });
        if (this._mouseDown) {
            var minDate = this._rangeStart < this._rangeEnd ? this._rangeStart : this._rangeEnd;
            var maxDate = this._rangeEnd > this._rangeStart ? this._rangeEnd : this._rangeStart;
            this.element.querySelectorAll('.month-container').forEach(function (month) {
                var monthId = parseInt(month.dataset.monthId);
                if (minDate.getMonth() <= monthId && maxDate.getMonth() >= monthId) {
                    month.querySelectorAll('td.day:not(.old):not(.new)').forEach(function (day) {
                        var date = _this._getDate(day);
                        if (date >= minDate && date <= maxDate) {
                            day.classList.add('range');
                            if (date.getTime() == minDate.getTime()) {
                                day.classList.add('range-start');
                            }
                            if (date.getTime() == maxDate.getTime()) {
                                day.classList.add('range-end');
                            }
                        }
                    });
                }
            });
        }
    };
    Calendar.prototype._getElementPosition = function (element) {
        var top = 0, left = 0;
        do {
            top += element.offsetTop || 0;
            left += element.offsetLeft || 0;
            element = element.offsetParent;
        } while (element);
        return { top: top, left: left };
    };
    Calendar.prototype._openContextMenu = function (elt) {
        var _this = this;
        var contextMenu = document.querySelector('.calendar-context-menu');
        if (contextMenu !== null) {
            contextMenu.style.display = 'none';
            // Clear the context menu (faster method)
            while (contextMenu.firstChild) {
                contextMenu.removeChild(contextMenu.firstChild);
            }
        }
        else {
            contextMenu = document.createElement('div');
            contextMenu.classList.add('calendar-context-menu');
            document.body.appendChild(contextMenu);
        }
        var date = this._getDate(elt);
        var events = this.getEvents(date);
        for (var i = 0; i < events.length; i++) {
            var eventItem = document.createElement('div');
            eventItem.classList.add('item');
            eventItem.style.paddingLeft = '4px';
            eventItem.style.boxShadow = "inset 4px 0 0 0 " + events[i].color;
            var eventItemContent = document.createElement('div');
            eventItemContent.classList.add('content');
            var text = document.createElement('span');
            text.classList.add('text');
            text.textContent = events[i].name;
            eventItemContent.appendChild(text);
            var icon = document.createElement('span');
            icon.classList.add('arrow');
            icon.innerHTML = "&rsaquo;";
            eventItemContent.appendChild(icon);
            eventItem.appendChild(eventItemContent);
            this._renderContextMenuItems(eventItem, this.options.contextMenuItems, events[i]);
            contextMenu.appendChild(eventItem);
        }
        if (contextMenu.children.length > 0) {
            var position = this._getElementPosition(elt);
            contextMenu.style.left = (position.left + 25) + 'px';
            contextMenu.style.right = '';
            contextMenu.style.top = (position.top + 25) + 'px';
            contextMenu.style.display = 'block';
            if (contextMenu.getBoundingClientRect().right > document.body.offsetWidth) {
                contextMenu.style.left = '';
                contextMenu.style.right = '0';
            }
            // Launch the position check once the whole context menu tree will be rendered
            setTimeout(function () { return _this._checkContextMenuItemsPosition(); }, 0);
            var closeContextMenu_1 = function (event) {
                if (event.type !== 'click' || !contextMenu.contains(event.target)) {
                    contextMenu.style.display = 'none';
                    window.removeEventListener('click', closeContextMenu_1);
                    window.removeEventListener('resize', closeContextMenu_1);
                    window.removeEventListener('scroll', closeContextMenu_1);
                }
            };
            window.addEventListener('click', closeContextMenu_1);
            window.addEventListener('resize', closeContextMenu_1);
            window.addEventListener('scroll', closeContextMenu_1);
        }
    };
    Calendar.prototype._renderContextMenuItems = function (parent, items, evt) {
        var subMenu = document.createElement('div');
        subMenu.classList.add('submenu');
        for (var i = 0; i < items.length; i++) {
            if (items[i].visible === false || (typeof items[i].visible === "function" && !items[i].visible(evt))) {
                continue;
            }
            var menuItem = document.createElement('div');
            menuItem.classList.add('item');
            var menuItemContent = document.createElement('div');
            menuItemContent.classList.add('content');
            var text = document.createElement('span');
            text.classList.add('text');
            text.textContent = items[i].text;
            menuItemContent.appendChild(text);
            if (items[i].click) {
                (function (index) {
                    menuItemContent.addEventListener('click', function () {
                        document.querySelector('.calendar-context-menu').style.display = 'none';
                        items[index].click(evt);
                    });
                })(i);
            }
            menuItem.appendChild(menuItemContent);
            if (items[i].items && items[i].items.length > 0) {
                var icon = document.createElement('span');
                icon.classList.add('arrow');
                icon.innerHTML = "&rsaquo;";
                menuItemContent.appendChild(icon);
                this._renderContextMenuItems(menuItem, items[i].items, evt);
            }
            subMenu.appendChild(menuItem);
        }
        if (subMenu.children.length > 0) {
            parent.appendChild(subMenu);
        }
    };
    Calendar.prototype._checkContextMenuItemsPosition = function () {
        var menus = document.querySelectorAll('.calendar-context-menu .submenu');
        menus.forEach(function (menu) {
            var htmlMenu = menu;
            htmlMenu.style.display = 'block';
            htmlMenu.style.visibility = 'hidden';
        });
        menus.forEach(function (menu) {
            var htmlMenu = menu;
            if (htmlMenu.getBoundingClientRect().right > document.body.offsetWidth) {
                htmlMenu.classList.add('open-left');
            }
            else {
                htmlMenu.classList.remove('open-left');
            }
        });
        menus.forEach(function (menu) {
            var htmlMenu = menu;
            htmlMenu.style.display = '';
            htmlMenu.style.visibility = '';
        });
    };
    Calendar.prototype._getDate = function (elt) {
        var day = elt.querySelector('.day-content').textContent;
        var month = elt.closest('.month-container').dataset.monthId;
        var year = this.options.startYear;
        return new Date(year, month, day);
    };
    Calendar.prototype._triggerEvent = function (eventName, parameters) {
        var event = null;
        if (typeof Event === "function") {
            event = new Event(eventName);
        }
        else {
            event = document.createEvent('Event');
            event.initEvent(eventName, false, false);
        }
        event.calendar = this;
        for (var i in parameters) {
            event[i] = parameters[i];
        }
        this.element.dispatchEvent(event);
        return event;
    };
    Calendar.prototype._isDisabled = function (date) {
        if ((this.options.minDate != null && date < this.options.minDate) || (this.options.maxDate != null && date > this.options.maxDate)) {
            return true;
        }
        if (this.options.disabledWeekDays.length > 0) {
            for (var d = 0; d < this.options.disabledWeekDays.length; d++) {
                if (date.getDay() == this.options.disabledWeekDays[d]) {
                    return true;
                }
            }
        }
        if (this.options.disabledDays.length > 0) {
            for (var d = 0; d < this.options.disabledDays.length; d++) {
                if (date.getTime() == this.options.disabledDays[d].getTime()) {
                    return true;
                }
            }
        }
        return false;
    };
    Calendar.prototype._isHidden = function (day) {
        if (this.options.hiddenWeekDays.length > 0) {
            for (var d = 0; d < this.options.hiddenWeekDays.length; d++) {
                if (day == this.options.hiddenWeekDays[d]) {
                    return true;
                }
            }
        }
        return false;
    };
    /**
     * Gets the week number for a specified date.
     *
     * @param date The specified date.
     */
    Calendar.prototype.getWeekNumber = function (date) {
        // Algorithm from https://weeknumber.net/how-to/javascript
        var workingDate = new Date(date.getTime());
        workingDate.setHours(0, 0, 0, 0);
        // Thursday in current week decides the year.
        workingDate.setDate(workingDate.getDate() + 3 - (workingDate.getDay() + 6) % 7);
        // January 4 is always in week 1.
        var week1 = new Date(workingDate.getFullYear(), 0, 4);
        // Adjust to Thursday in week 1 and count number of weeks from date to week1.
        return 1 + Math.round(((workingDate.getTime() - week1.getTime()) / 86400000
            - 3 + (week1.getDay() + 6) % 7) / 7);
    };
    /**
     * Gets the data source elements for a specified day.
     *
     * @param date The specified day.
     */
    Calendar.prototype.getEvents = function (date) {
        return this.getEventsOnRange(date, new Date(date.getFullYear(), date.getMonth(), date.getDate() + 1));
    };
    /**
     * Gets the data source elements for a specified range of days.
     *
     * @param startDate The beginning of the day range (inclusive).
     * @param endDate The end of the day range (non inclusive).
     */
    Calendar.prototype.getEventsOnRange = function (startDate, endDate) {
        var events = [];
        if (this._dataSource && startDate && endDate) {
            for (var i = 0; i < this._dataSource.length; i++) {
                if (this._dataSource[i].startDate < endDate && this._dataSource[i].endDate >= startDate) {
                    events.push(this._dataSource[i]);
                }
            }
        }
        return events;
    };
    /**
     * Check if there is no event on the first part, last part or on the whole specified day.
     *
     * @param date The specified day.
     * @param after Whether to check for a free slot on the first part (if `false`) or the last part (if `true`) of the day. If `null`, this will check on the whole day.
     *
     * Usefull only if using the `alwaysHalfDay` option of the calendar, or the `startHalfDay` or `endHalfDay` option of the datasource.
     */
    Calendar.prototype.isThereFreeSlot = function (date, after) {
        var _this = this;
        if (after === void 0) { after = null; }
        var events = this.getEvents(date);
        if (after === true) {
            return !events.some(function (evt) { return (!_this.options.alwaysHalfDay && !evt.endHalfDay) || evt.endDate > date; });
        }
        else if (after === false) {
            return !events.some(function (evt) { return (!_this.options.alwaysHalfDay && !evt.startHalfDay) || evt.startDate < date; });
        }
        else {
            return this.isThereFreeSlot(date, false) || this.isThereFreeSlot(date, true);
        }
    };
    /**
     * Gets the year displayed on the calendar.
     */
    Calendar.prototype.getYear = function () {
        return this.options.startYear;
    };
    /**
     * Sets the year displayed on the calendar.
     *
     * @param year The year to displayed on the calendar.
     */
    Calendar.prototype.setYear = function (year) {
        var _this = this;
        var parsedYear = parseInt(year);
        if (!isNaN(parsedYear)) {
            this.options.startYear = parsedYear;
            // Clear the calendar (faster method)
            while (this.element.firstChild) {
                this.element.removeChild(this.element.firstChild);
            }
            if (this.options.displayHeader) {
                this._renderHeader();
            }
            var eventResult = this._triggerEvent('yearChanged', { currentYear: this.options.startYear, preventRendering: false });
            if (typeof this.options.dataSource === "function") {
                this.render(true);
                this._fetchDataSource(function (dataSource) {
                    _this._dataSource = dataSource;
                    _this._initializeDatasourceColors();
                    _this.render(false);
                });
            }
            else {
                if (!eventResult.preventRendering) {
                    this.render();
                }
            }
        }
    };
    /**
     * Gets the minimum date of the calendar.
     */
    Calendar.prototype.getMinDate = function () {
        return this.options.minDate;
    };
    /**
     * Sets the minimum date of the calendar.
     *
     * This method causes a refresh of the calendar.
     *
     * @param minDate The minimum date to set.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setMinDate = function (date, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        if (date instanceof Date || date === null) {
            this.options.minDate = date;
            if (!preventRendering) {
                this.render();
            }
        }
    };
    /**
     * Gets the maximum date of the calendar.
     */
    Calendar.prototype.getMaxDate = function () {
        return this.options.maxDate;
    };
    /**
     * Sets the maximum date of the calendar.
     *
     * This method causes a refresh of the calendar.
     *
     * @param maxDate The maximum date to set.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setMaxDate = function (date, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        if (date instanceof Date || date === null) {
            this.options.maxDate = date;
            if (!preventRendering) {
                this.render();
            }
        }
    };
    /**
     * Gets the current style used for displaying data source.
     */
    Calendar.prototype.getStyle = function () {
        return this.options.style;
    };
    /**
     * Sets the style to use for displaying data source.
     *
     * This method causes a refresh of the calendar.
     *
     * @param style The style to use for displaying data source ("background", "border" or "custom").
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setStyle = function (style, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.style = style == 'background' || style == 'border' || style == 'custom' ? style : 'border';
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets a value indicating whether the user can select a range which overlapping an other element present in the datasource.
     */
    Calendar.prototype.getAllowOverlap = function () {
        return this.options.allowOverlap;
    };
    /**
     * Sets a value indicating whether the user can select a range which overlapping an other element present in the datasource.
     *
     * @param allowOverlap Indicates whether the user can select a range which overlapping an other element present in the datasource.
     */
    Calendar.prototype.setAllowOverlap = function (allowOverlap) {
        this.options.allowOverlap = allowOverlap;
    };
    /**
     * Gets a value indicating whether the weeks number are displayed.
     */
    Calendar.prototype.getDisplayWeekNumber = function () {
        return this.options.displayWeekNumber;
    };
    /**
     * Sets a value indicating whether the weeks number are displayed.
     *
     * This method causes a refresh of the calendar.
     *
     * @param  displayWeekNumber Indicates whether the weeks number are displayed.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setDisplayWeekNumber = function (displayWeekNumber, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.displayWeekNumber = displayWeekNumber;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets a value indicating whether the calendar header is displayed.
     */
    Calendar.prototype.getDisplayHeader = function () {
        return this.options.displayHeader;
    };
    /**
     * Sets a value indicating whether the calendar header is displayed.
     *
     * This method causes a refresh of the calendar.
     *
     * @param displayHeader Indicates whether the calendar header is displayed.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setDisplayHeader = function (displayHeader, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.displayHeader = displayHeader;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets a value indicating whether the data source must be rendered on disabled days.
     */
    Calendar.prototype.getDisplayDisabledDataSource = function () {
        return this.options.displayDisabledDataSource;
    };
    /**
     * Sets a value indicating whether the data source must be rendered on disabled days.
     *
     * This method causes a refresh of the calendar.
     *
     * @param displayDisabledDataSource Indicates whether the data source must be rendered on disabled days.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setDisplayDisabledDataSource = function (displayDisabledDataSource, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.displayDisabledDataSource = displayDisabledDataSource;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets a value indicating whether the beginning and the end of each range should be displayed as half selected day.
     */
    Calendar.prototype.getAlwaysHalfDay = function () {
        return this.options.alwaysHalfDay;
    };
    /**
     * Sets a value indicating whether the beginning and the end of each range should be displayed as half selected day.
     *
     * This method causes a refresh of the calendar.
     *
     * @param alwaysHalfDay Indicates whether the beginning and the end of each range should be displayed as half selected day.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setAlwaysHalfDay = function (alwaysHalfDay, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.alwaysHalfDay = alwaysHalfDay;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets a value indicating whether the user can make range selection.
     */
    Calendar.prototype.getEnableRangeSelection = function () {
        return this.options.enableRangeSelection;
    };
    /**
     * Sets a value indicating whether the user can make range selection.
     *
     * This method causes a refresh of the calendar.
     *
     * @param enableRangeSelection Indicates whether the user can make range selection.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setEnableRangeSelection = function (enableRangeSelection, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.enableRangeSelection = enableRangeSelection;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets the disabled days.
     */
    Calendar.prototype.getDisabledDays = function () {
        return this.options.disabledDays;
    };
    /**
     * Sets the disabled days.
     *
     * This method causes a refresh of the calendar.
     *
     * @param disableDays The disabled days to set.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setDisabledDays = function (disabledDays, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.disabledDays = disabledDays instanceof Array ? disabledDays : [];
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets the disabled days of the week.
     */
    Calendar.prototype.getDisabledWeekDays = function () {
        return this.options.disabledWeekDays;
    };
    /**
     * Sets the disabled days of the week.
     *
     * This method causes a refresh of the calendar.
     *
     * @param disabledWeekDays The disabled days of the week to set.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setDisabledWeekDays = function (disabledWeekDays, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.disabledWeekDays = disabledWeekDays instanceof Array ? disabledWeekDays : [];
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets the hidden days of the week.
     */
    Calendar.prototype.getHiddenWeekDays = function () {
        return this.options.hiddenWeekDays;
    };
    /**
     * Sets the hidden days of the week.
     *
     * This method causes a refresh of the calendar.
     *
     * @param hiddenWeekDays The hidden days of the week to set.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setHiddenWeekDays = function (hiddenWeekDays, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.hiddenWeekDays = hiddenWeekDays instanceof Array ? hiddenWeekDays : [];
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets a value indicating whether the beginning and the end of each range should be displayed as rounded cells.
     */
    Calendar.prototype.getRoundRangeLimits = function () {
        return this.options.roundRangeLimits;
    };
    /**
     * Sets a value indicating whether the beginning and the end of each range should be displayed as rounded cells.
     *
     * This method causes a refresh of the calendar.
     *
     * @param roundRangeLimits Indicates whether the beginning and the end of each range should be displayed as rounded cells.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setRoundRangeLimits = function (roundRangeLimits, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.roundRangeLimits = roundRangeLimits;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets a value indicating whether the default context menu must be displayed when right clicking on a day.
     */
    Calendar.prototype.getEnableContextMenu = function () {
        return this.options.enableContextMenu;
    };
    /**
     * Sets a value indicating whether the default context menu must be displayed when right clicking on a day.
     *
     * This method causes a refresh of the calendar.
     *
     * @param enableContextMenu Indicates whether the default context menu must be displayed when right clicking on a day.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setEnableContextMenu = function (enableContextMenu, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.enableContextMenu = enableContextMenu;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets the context menu items.
     */
    Calendar.prototype.getContextMenuItems = function () {
        return this.options.contextMenuItems;
    };
    /**
     * Sets new context menu items.
     *
     * This method causes a refresh of the calendar.
     *
     * @param contextMenuItems The new context menu items.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setContextMenuItems = function (contextMenuItems, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.contextMenuItems = contextMenuItems instanceof Array ? contextMenuItems : [];
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets the custom day renderer.
     */
    Calendar.prototype.getCustomDayRenderer = function () {
        return this.options.customDayRenderer;
    };
    /**
     * Sets the custom day renderer.
     *
     * This method causes a refresh of the calendar.
     *
     * @param handler The function used to render the days. This function is called during render for each day.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setCustomDayRenderer = function (customDayRenderer, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.customDayRenderer = typeof customDayRenderer === "function" ? customDayRenderer : null;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets the custom data source renderer.
     */
    Calendar.prototype.getCustomDataSourceRenderer = function () {
        return this.options.customDataSourceRenderer;
    };
    /**
     * Sets the custom data source renderer. Works only with the style set to "custom".
     *
     * This method causes a refresh of the calendar.
     *
     * @param handler The function used to render the data source. This function is called during render for each day containing at least one event.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setCustomDataSourceRenderer = function (customDataSourceRenderer, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.customDataSourceRenderer = typeof customDataSourceRenderer === "function" ? customDataSourceRenderer : null;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets the language used for calendar rendering.
     */
    Calendar.prototype.getLanguage = function () {
        return this.options.language;
    };
    /**
     * Sets the language used for calendar rendering.
     *
     * This method causes a refresh of the calendar.
     *
     * @param language The language to use for calendar redering.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setLanguage = function (language, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        if (language != null && Calendar.locales[language] != null) {
            this.options.language = language;
            if (!preventRendering) {
                this.render();
            }
        }
    };
    /**
     * Gets the current data source.
     */
    Calendar.prototype.getDataSource = function () {
        return this.options.dataSource;
    };
    /**
     * Sets a new data source.
     *
     * This method causes a refresh of the calendar.
     *
     * @param dataSource The new data source.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setDataSource = function (dataSource, preventRendering) {
        var _this = this;
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.dataSource = dataSource instanceof Array || typeof dataSource === "function" ? dataSource : [];
        if (typeof this.options.dataSource === "function") {
            this.render(true);
            this._fetchDataSource(function (dataSource) {
                _this._dataSource = dataSource;
                _this._initializeDatasourceColors();
                _this.render(false);
            });
        }
        else {
            this._dataSource = this.options.dataSource;
            this._initializeDatasourceColors();
            if (!preventRendering) {
                this.render();
            }
        }
    };
    /**
     * Gets the starting day of the week.
     */
    Calendar.prototype.getWeekStart = function () {
        return this.options.weekStart ? this.options.weekStart : Calendar.locales[this.options.language].weekStart;
    };
    /**
     * Sets the starting day of the week.
     *
     * This method causes a refresh of the calendar.
     *
     * @param weekStart The starting day of the week. This option overrides the parameter define in the language file.
     * @param preventRedering Indicates whether the rendering should be prevented after the property update.
     */
    Calendar.prototype.setWeekStart = function (weekStart, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this.options.weekStart = !isNaN(parseInt(weekStart)) ? parseInt(weekStart) : null;
        if (!preventRendering) {
            this.render();
        }
    };
    /**
     * Gets the loading template.
     */
    Calendar.prototype.getLoadingTemplate = function () {
        return this.options.loadingTemplate;
    };
    /**
     * Sets the loading template.
     *
     * @param loadingTemplate The loading template.
     */
    Calendar.prototype.setLoadingTemplate = function (loadingTemplate) {
        this.options.loadingTemplate = typeof loadingTemplate === "string" || loadingTemplate instanceof HTMLElement ? loadingTemplate : null;
    };
    /**
     *
     * Add a new element to the data source.
     *
     * This method causes a refresh of the calendar.
     *
     * @param element The element to add.
     * @param preventRendering Indicates whether the calendar shouldn't be refreshed once the event added.
     */
    Calendar.prototype.addEvent = function (evt, preventRendering) {
        if (preventRendering === void 0) { preventRendering = false; }
        this._dataSource.push(evt);
        if (!preventRendering) {
            this.render();
        }
    };
    Calendar.locales = {
        en: {
            days: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            daysShort: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
            daysMin: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"],
            months: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
            monthsShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            weekShort: 'W',
            weekStart: 0
        }
    };
    Calendar.colors = ['#2C8FC9', '#9CB703', '#F5BB00', '#FF4A32', '#B56CE2', '#45A597'];
    return Calendar;
}());
exports["default"] = Calendar;
if (typeof window === "object") {
    window.Calendar = Calendar;
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll('[data-provide="calendar"]').forEach(function (element) { return new Calendar(element); });
    });
}
