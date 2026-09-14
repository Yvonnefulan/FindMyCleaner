// Uses the deployed Render API.
const API_URL = 'https://findmycleaner.onrender.com/api/CleaningServices';
const reloadButton = document.getElementById('reload');
const statusElement = document.getElementById('status');
const results = document.getElementById('results');
const tbody = document.getElementById('services');
const count = document.getElementById('count');
const updated = document.getElementById('updated');
const panel = document.getElementById('panel');
const currency = new Intl.NumberFormat('en-AU', { style: 'currency', currency: 'AUD' });

function formatList(value) {
  return Array.isArray(value) && value.length ? value.join(', ') : '—';
}

function formatDate(value) {
  const date = value ? new Date(value) : null;
  return date && !Number.isNaN(date.getTime()) ? date.toLocaleString('en-AU') : '—';
}

function appendCell(row, value, className) {
  const cell = document.createElement('td');
  // API values are rendered as text, never interpreted as HTML.
  cell.textContent = value === null || value === undefined || value === '' ? '—' : String(value);
  if (className) cell.className = className;
  row.appendChild(cell);
}

function appendAvailability(row, value) {
  const cell = document.createElement('td');
  const badge = document.createElement('span');
  badge.className = value === true ? 'badge' : 'badge off';
  badge.textContent = value === true ? 'Available' : value === false ? 'Unavailable' : '—';
  cell.appendChild(badge);
  row.appendChild(cell);
}

function renderServices(services) {
  const fragment = document.createDocumentFragment();
  for (const service of services) {
    const row = document.createElement('tr');
    for (const key of ['serviceProvider', 'serviceName', 'serviceType', 'suburb']) appendCell(row, service[key]);
    appendCell(row, typeof service.priceFrom === 'number' ? currency.format(service.priceFrom) : '—');
    appendCell(row, typeof service.minDurationHours === 'number' ? `${service.minDurationHours} hours` : '—');
    appendAvailability(row, service.isAvailable);
    appendAvailability(row, service.isNightShiftAvailable);
    appendCell(row, formatList(service.availableDay));
    appendCell(row, formatList(service.keywords));
    appendCell(row, formatDate(service.createdDate));
    appendCell(row, service.id ?? service.Id, 'id');
    fragment.appendChild(row);
  }
  tbody.replaceChildren(fragment);
}

async function loadServices() {
  reloadButton.disabled = true;
  panel.setAttribute('aria-busy', 'true');
  statusElement.hidden = false;
  statusElement.className = '';
  statusElement.textContent = 'Loading cleaning services…';
  results.hidden = true;
  tbody.replaceChildren();
  count.textContent = 'All services';
  updated.textContent = '';
  const controller = new AbortController();
  const timeout = setTimeout(() => controller.abort(), 90000);

  try {
    const response = await fetch(API_URL, {
      method: 'GET',
      headers: { Accept: 'application/json', 'api-version': '2.0' },
      signal: controller.signal,
      cache: 'no-store'
    });

    // This API returns this specific 404 message for an empty collection.
    let services;
    if (response.status === 404 && (await response.clone().text()).trim() === 'No cleaning services found.') {
      services = [];
    } else {
      if (!response.ok) throw new Error(`Request failed (HTTP ${response.status}). Check that the API is running and try again.`);
      services = await response.json();
    }
    if (!Array.isArray(services) || services.some(service => !service || typeof service !== 'object' || Array.isArray(service))) {
      throw new Error('The API returned an unexpected format. An array of cleaning services was expected.');
    }

    renderServices(services);
    count.textContent = `All services · ${services.length}`;
    updated.textContent = `Updated: ${new Date().toLocaleTimeString('en-AU')}`;
    statusElement.textContent = services.length ? `Loaded ${services.length} services.` : 'No cleaning services are currently available.';
    results.hidden = services.length === 0;
  } catch (error) {
    statusElement.className = 'error';
    statusElement.textContent = error.name === 'AbortError'
      ? 'The request timed out. Check that the API is running and reload the page.'
      : error instanceof TypeError
        ? 'Unable to connect to the API. Check your internet connection and try Reload.'
        : error instanceof SyntaxError ? 'The API response was not valid JSON.' : error.message;
    count.textContent = 'Loading failed';
  } finally {
    clearTimeout(timeout);
    reloadButton.disabled = false;
    panel.setAttribute('aria-busy', 'false');
  }
}

reloadButton.addEventListener('click', loadServices);
loadServices();
