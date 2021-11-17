#include "pch.h"
#include <amp.h>
#include <amp_math.h>

#define EXPORT extern "C" __declspec(dllexport)

using namespace concurrency;
using std::vector;

EXPORT array_view<UINT, 2>* GetArrayView(UINT* bmp, int width, int height)
{
	return new array_view<UINT, 2>(height, width, bmp);
}
EXPORT array_view<UINT, 2>* GetArrayViewPolygons(float* polygons, int count)
{
	return new array_view<UINT, 2>(count, 9, polygons);
}
EXPORT void DeleteArrayView(array_view<UINT, 2>* abmp)
{
	delete abmp;
}

int vvv(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3) restrict(amp) 
{
	if ((x1 * y2 * z3 + x2 * y3 * z1 + x3 * y1 * z2) - (x3 * y2 * z1 + x2 * y1 * z3 + x1 * y3 * z2) > 0) {
		return 1;
	}
	else {
		return -1;
	}
}

EXPORT void RayCast(array_view<UINT, 2>& abmp, array_view<const UINT, 2>& polygons)
{
	abmp.refresh();
	polygons.refresh();

	float cx = 0, cy = 0, cz = 1, cax = 0, cay = 0, caz = 0;
	float scax = 0, scay = 0, scaz = -1;

	parallel_for_each(abmp.extent, [=](index<2> idx) restrict(amp)
		{
			float dx = 4.0f * (abmp.extent[1] / 2 - idx[1]) / abmp.extent[1];
			float dy = 4.0f * (abmp.extent[0] / 2 - idx[0]) / abmp.extent[0];

			float vx = scax + dx;
			float vy = scay + dy;
			float vz = scaz;

			//Z rot
			{
				float nvx = vx * fast_math::cos(caz) - vy * fast_math::sin(caz);
				float nvy = vy * fast_math::cos(caz) + vx * fast_math::sin(caz);

				float vx = nvx;
				float vy = nvy;
			}
			//Y rot
			{
				float nvz = vz * fast_math::cos(cay) - vx * fast_math::sin(cay);
				float nvx = vx * fast_math::cos(cay) + vz * fast_math::sin(cay);

				float vz = nvz;
				float vx = nvx;
			}
			//X rot
			{
				float nvy = vy * fast_math::cos(cax) - vz * fast_math::sin(cax);
				float nvz = vz * fast_math::cos(cax) + vy * fast_math::sin(cax);

				float vy = nvy;
				float vz = nvz;
			}

			for (int i = 0; i < polygons.extent[0]; i++) 
			{
				int p1 = 0;
				
			}

		});
	abmp.synchronize();
}
EXPORT void Clear(array_view<UINT, 2>& abmp, int width, int height, UINT color)
{
	abmp.refresh();

	parallel_for_each(abmp.extent, [=](index<2> idx) restrict(amp)
		{
			abmp[idx[0]][idx[1]] = color;
		});
	abmp.synchronize();
}